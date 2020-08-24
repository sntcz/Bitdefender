using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDModule.JsonRpc
{
    /// <summary>
    /// JSON-RPC 2.0 request model.
    /// </summary>
    /// <typeparam name="TParams">Type of the Params object.</typeparam>
    internal sealed class JsonRpcRequest<TParams>
    {
        [JsonProperty("id")]
        public string Id { get; } = Guid.NewGuid().ToString();

        [JsonProperty("jsonrpc", Required = Required.Always)]
        public string Version => "2.0";

        [JsonProperty("method", Required = Required.Always)]
        public string Method { get; set; }

        [JsonProperty("params")]
        public TParams Params { get; set; }

        public JsonRpcRequest()
        { /* NOP */ }

        public JsonRpcRequest(string method, TParams pars)
        {
            Method = method;
            Params = pars;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
