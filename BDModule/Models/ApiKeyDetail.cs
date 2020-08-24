using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDModule.Models
{
    /// <summary>
    /// <para type="description">The details of the API key.</para>
    /// </summary>
    [Serializable]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields", Justification = "<Pending>")]
    public sealed class ApiKeyDetail
    {
        /// <summary>
        /// <para type="description">An Array containing the list of enabled APIs.</para>
        /// </summary>
        [JsonProperty("enabledApis")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "<Pending>")]
        public string[] EnabledAPIs { get; set; }
        /// <summary>
        /// <para type="description">The UTC date and time when the API key was generated.</para>
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
