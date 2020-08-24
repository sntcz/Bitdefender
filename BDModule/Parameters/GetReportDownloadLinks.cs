using Newtonsoft.Json;
using System;

namespace BDModule.Parameters
{
    /// <summary>
    /// TBD
    /// </summary>
    [Serializable]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields", Justification = "<Pending>")]
    public sealed class GetReportDownloadLinks
    {
        /// <summary>
        /// The report ID.
        /// </summary>
        [JsonProperty("reportId")]
        public string ReportId { get; set; }
    }
}
