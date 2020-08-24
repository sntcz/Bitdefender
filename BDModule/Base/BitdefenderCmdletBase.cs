using BDModule.JsonRpc;
using BDModule.Parameters;
using Newtonsoft.Json;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Management.Automation;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BDModule.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TParams"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class BitdefenderCmdletBase<TParams, TResult> : PSCmdlet
        where TParams : class
        //where TResult : class
    {
        private const string API_URL = "/api/v1.0/jsonrpc/";
        private const string DEFAULT_ACCESS_URL = @"https://cloud.gravityzone.bitdefender.com";

#if FIXRESOLVING
        private static Assembly newtonsoftAssembly;
#endif

        private readonly MediaTypeHeaderValue jsonMediaType = new MediaTypeHeaderValue("application/json");
        private readonly string relativeUrl;
        private readonly string method;

        private HttpClient httpClient;

        Uri serverUri;
        /// <summary>
        /// <para type="description">The base URL for all APIs is the machine hostname, domain or IP where GravityZone is installed.</para>
        /// </summary>
        [Parameter(
            HelpMessage = "The base URL for all APIs is the machine hostname, domain or IP where GravityZone is installed."
            )]
        //[Alias("AccessUrl", "Host")] - no aliases defined
        public string ServerUrl
        {
            get => serverUri?.OriginalString;
            set => serverUri = new Uri(value);
        }

        /// <summary>
        /// <para type="description">The API key is a unique key that is generated in MyAccount section of Bitdefender Control Center.</para>
        /// </summary>
        [Parameter(Mandatory = true, HelpMessage = "The API key is a unique key that is generated in MyAccount section of Bitdefender Control Center.")]
        public string ApiKey { get; set; }

        /// <summary>
        /// Bitdefender API relative url.
        /// </summary>
        protected virtual string ApiUrl => relativeUrl;
        /// <summary>
        /// Bitdefender API input parameters.
        /// </summary>
        protected abstract TParams Parameters { get; }

        internal BitdefenderCmdletBase(string relativeUrl, string method)
        {
            Contract.Assert(relativeUrl != null, "relativeUrl is null");
            Contract.Assert(!relativeUrl.StartsWith("/"), "relativeUrl starts with /");
            Contract.Assert(!relativeUrl.EndsWith("/"), "relativeUrl ends with /");
            Contract.Assert(method != null, "method name is null");
            this.relativeUrl = relativeUrl;
            this.method = method;
            ServerUrl = DEFAULT_ACCESS_URL;
        }

        /// <summary>
        /// This method gets called once for each cmdlet in the pipeline when the pipeline starts executing.
        /// </summary>
        protected override void BeginProcessing()
        {
            httpClient = new HttpClient();
            base.BeginProcessing();
#if FIXRESOLVING
            FixAssemblyResolving();
#endif
        }

        /// <summary>
        /// This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (PagingParameters == null)
            {
                Task<TResult> task = SendAsync<TParams, TResult>(Parameters, CancellationToken.None);
                TResult result = task.ConfigureAwait(true).GetAwaiter().GetResult();
                WriteObject(result);
            }
            else
            {
                TParams parms = Parameters;
                int perPage = 100;
                int page = (Int32)(PagingParameters.Skip / (UInt64)perPage) + 1;

                int skip = (Int32)(PagingParameters.Skip % (UInt64)perPage);
                ulong first = PagingParameters.First;
                bool includeTotalCount = PagingParameters.IncludeTotalCount;

                while (first > 0 || includeTotalCount)
                {
                    (parms as ParameterListBase).Page = page;
                    (parms as ParameterListBase).PerPage = perPage;

                    Task<Parameters.ReturnList<TResult>> task = SendAsync<TParams, Parameters.ReturnList<TResult>>(Parameters, CancellationToken.None);
                    Parameters.ReturnList<TResult> result = task.ConfigureAwait(true).GetAwaiter().GetResult();

                    if (includeTotalCount)
                    {
                        includeTotalCount = false;
                        // when using data sources to retrieve results,
                        //  (1) some data sources might have the exact number of results retrieved and in this case would have accuracy 1.0
                        //  (2) some data sources might only have an estimate and in this case would use accuracy between 0.0 and 1.0
                        //  (3) other data sources might not know how many items there are in total and in this case would use accuracy 0.0
                        double accuracy = 1.0;
                        PSObject totalCountResult = PagingParameters.NewTotalCount((UInt64)result.Total, accuracy);
                        this.WriteObject(totalCountResult);
                    }

                    foreach (TResult item in result.Items)
                    {
                        if (skip > 0)
                            skip--;
                        else
                        {
                            if (first == 0)
                                break;
                            first--;
                            WriteObject(item);
                        }
                    }
                    if (result.Page >= result.PagesCount)
                        break;
                    page++;
                }
            }
        }

        /// <summary>
        /// This method will be called once at the end of pipeline execution; if no input is received, this method is not called.
        /// </summary>
        protected override void EndProcessing()
        {
            base.EndProcessing();
            httpClient.Dispose();
#if FIXRESOLVING
            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
#endif
        }

        /// <summary>
        /// Send JSON-RPC 2.0 request to the TargetUrl.
        /// </summary>
        /// <typeparam name="TSendParams">Parameters type. Could be JToken or other JSON-serializable type.</typeparam>
        /// <typeparam name="TSendResult">Result type. Should be deserializable by JSON.NET.</typeparam>
        /// <param name="args">JSON-RPC 2.0 method parameters.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>JSON-RPC 2.0 response result if the responce doesn't contain an error.</returns>
        /// <exception cref="JsonRpcError">Throws if JSON-RPC 2.0 response contains an error.</exception>
        protected async Task<TSendResult> SendAsync<TSendParams, TSendResult>(TSendParams args, CancellationToken ct)
        {
            var jsonRequest = new JsonRpcRequest<TSendParams>()
            {
                Method = method,
                Params = args
            };

            string contentString = JsonConvert.SerializeObject(jsonRequest, Formatting.Indented);
            StringContent content = new StringContent(contentString);
            content.Headers.ContentType = jsonMediaType;
            string authString = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ApiKey}:"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

            Uri targetUri = new Uri(new Uri(serverUri, API_URL), ApiUrl);
            WriteVerbose($"Server url: {serverUri}");

            using (var response = await httpClient.PostAsync(targetUri, content, ct).ConfigureAwait(true))
            {
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                var jsonResponse = JsonConvert.DeserializeObject<JsonRpcResponse<TSendResult>>(result);

                Contract.Assert(jsonRequest.Id == jsonResponse.Id, "Request and response id is not same");

                if (jsonResponse.Error != null)
                {
                    throw new JsonRpcException(jsonResponse.Error);
                }
                return jsonResponse.Result;
            }
        }

#if FIXRESOLVING
        private void FixAssemblyResolving()
        {
            if (newtonsoftAssembly == null)
            {
                newtonsoftAssembly = GetAssembly("Newtonsoft.Json.dll");
            }

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private Assembly GetAssembly(string assemblyName)
        {
            Assembly assembly = null;
            string assemblyPath = Path.Combine(AssemblyDirectoryFromLocation, assemblyName);
            WriteDebug($"Assembly path: {assemblyPath}");
            if (File.Exists(assemblyPath))
            {
                assembly = Assembly.LoadFrom(assemblyPath);
            }
            else
            {
                string codebasePath = Path.Combine(AssemblyDirectoryFromCodeBase, assemblyName);
                WriteDebug($"Codebase path: {codebasePath}");
                assembly = Assembly.LoadFrom(codebasePath);
            }
            return assembly;
        }

        private string AssemblyDirectoryFromLocation
        {
            get
            {
                var location = Assembly.GetExecutingAssembly().Location;
                var escapedLocation = Uri.UnescapeDataString(location);
                return Path.GetDirectoryName(escapedLocation);
            }
        }

        private string AssemblyDirectoryFromCodeBase
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // WriteDebug($"Assembly resolve {args.Name}");
            if (args.Name.StartsWith("NewtonSoft.Json", StringComparison.InvariantCultureIgnoreCase) && newtonsoftAssembly != null)
            {
                return newtonsoftAssembly;
            }
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName == args.Name)
                {
                    return assembly;
                }
            }
            return null;
        }
#endif

    }
}
