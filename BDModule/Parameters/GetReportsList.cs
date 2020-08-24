using BDModule.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDModule.Parameters
{
    /// <summary>
    /// Get report list input parameter.
    /// </summary>
    [Serializable]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields", Justification = "<Pending>")]
    public sealed class GetReportsList : ParameterListBase
    {
        /// <summary>
        /// The name of the report.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The report type. Mandatory.
        /// </summary>
        [JsonProperty("type")]
        public ReportType Type { get; set; }
    }
}
