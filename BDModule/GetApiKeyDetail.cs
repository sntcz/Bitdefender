using System;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;
using BDModule.Models;

namespace BDModule
{
    /// <summary>
    /// <para type="synopsis">Get Bitdefender details about the API key used.</para>
    /// <para type="description">The API key is a unique key that is generated 
    /// in MyAccount section of Bitdefender Control Center. Each API key allows 
    /// the application to call methods exposed by one or several APIs. The allowed 
    /// APIs are selected at the time the API key is generated.</para>
    /// </summary>
    /// <example>
    /// <para>Get-BitdefenderApiKeyDetail -ApiKey 'YOURAPIKEY' -ServerUrl 'YOURSERVERURL'</para>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "BitdefenderApiKeyDetail")]
    [OutputType(typeof(ApiKeyDetail))]
    public class GetApiKeyDetail : Base.BitdefenderCmdletBase<object, ApiKeyDetail>
    {
        /// <summary>
        /// Bitdefender API input parameters - null.
        /// </summary>
        protected override object Parameters  => null; 

        /// <summary>
        /// 
        /// </summary>
        public GetApiKeyDetail() : base("general", "getApiKeyDetails")
        { /* NOP */ }
        
    }
}
