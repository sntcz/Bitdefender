using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Management.Automation;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BDModule.UnitTest
{
    // Inspired by https://d-fens.ch/2016/11/16/unit-testing-c-binary-powershell-modules/
    public class BDModuleCmdletTestBase
    {
        private const string POWERSHELL_CMDLET_NAME_FORMATSTRING = "{0}-{1}";

        protected IConfiguration Configuration { get; private set; }

        public BDModuleCmdletTestBase()
        {
            // Secrets: https://patrickhuber.github.io/2017/07/26/avoid-secrets-in-dot-net-core-tests.html
            // Run following commands from directory where .csproj file:
            //       dotnet user-secrets set ApiKey abc123
            //       dotnet user-secrets set ServerUrl https://YOUR-HOSTNAME
            // the type specified here is just so the secrets library can 
            // find the UserSecretId we added in the csproj file
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<BDModuleCmdletTestBase>();

            Configuration = builder.Build();
        }

        public IList<T> Invoke<T>(Type cmdletType)
        {
            return Invoke<T>(cmdletType, new Dictionary<string, object>());
        }

        public IList<T> Invoke<T>(Type cmdletType, string parameterName, object parameterValue)
        {
            return Invoke<T>(cmdletType, new Dictionary<string, object>()
            {
                { parameterName, parameterValue }
            });
        }

        public IList<T> Invoke<T>(Type cmdletType, string parameterName1, object parameterValue1, string parameterName2, object parameterValue2)
        {
            return Invoke<T>(cmdletType, new Dictionary<string, object>()
            {
                { parameterName1, parameterValue1 },
                { parameterName2, parameterValue2 }
            });
        }

        public IList<T> Invoke<T>(Type cmdletType, Dictionary<string, object> parameters)
        {
            using (PowerShell ps = CreatePowerShell(cmdletType, parameters))
            {
                return ps.Invoke<T>();
            }
        }

        public IList<PSObject> Invoke(Type cmdletType)
        {
            return Invoke(cmdletType, new Dictionary<string, object>());
        }

        public IList<PSObject> Invoke(Type cmdletType, string parameterName, object parameterValue)
        {
            return Invoke(cmdletType, new Dictionary<string, object>()
            {
                { parameterName, parameterValue }
            });
        }

        public IList<PSObject> Invoke(Type cmdletType, string parameterName1, object parameterValue1, string parameterName2, object parameterValue2)
        {
            return Invoke(cmdletType, new Dictionary<string, object>()
            {
                { parameterName1, parameterValue1 },
                { parameterName2, parameterValue2 }
            });
        }

        public IList<PSObject> Invoke(Type cmdletType, Dictionary<string, object> parameters)
        {
            using (PowerShell ps = CreatePowerShell(cmdletType, parameters))
            {
                return ps.Invoke();
            }            
        }


        protected PowerShell CreatePowerShell(Type cmdletType, Dictionary<string, object> parameters)
        {
            PowerShell ps = PowerShell.Create();

            ps.AddCommand("Import-Module").AddParameter("Assembly", cmdletType.Assembly);
            ps.Invoke();
            ps.Commands.Clear();

            Contract.Requires(null != cmdletType);
            Contract.Requires(null != parameters);
            Contract.Ensures(null != Contract.Result<IList<PSObject>>());

            // construct the Cmdlet name the type implements
            var cmdletAttribute = (CmdletAttribute)cmdletType.GetCustomAttributes(typeof(CmdletAttribute), true).Single();
            Contract.Assert(null != cmdletAttribute, typeof(CmdletAttribute).FullName);
            var cmdletName = string.Format(POWERSHELL_CMDLET_NAME_FORMATSTRING, cmdletAttribute.VerbName, cmdletAttribute.NounName);

            var cmdletNameToInvoke = cmdletName;
            Contract.Assert(!string.IsNullOrWhiteSpace(cmdletNameToInvoke));

            ps.AddCommand(cmdletNameToInvoke);

            string bitdefenderApiKey = Configuration["ApiKey"];
            if (String.IsNullOrEmpty(bitdefenderApiKey))
                throw new Exception("Configure user secrets, run command 'dotnet user-secrets set ApiKey abc123' to run tests.");

            foreach (var item in parameters)
            {
                ps.AddParameter(item.Key, item.Value);
            }

            ps.AddParameter("ApiKey", bitdefenderApiKey);
            string bitdefenderServerUrl = Configuration["ServerUrl"];
            if (!String.IsNullOrWhiteSpace(bitdefenderServerUrl))
                ps.AddParameter("ServerUrl", bitdefenderServerUrl);

            return ps;
        }
    }
}
