using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDModule.Models
{
    /// <summary>
    /// <para type="description">The information about the report.</para>
    /// </summary>
    [Serializable]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields", Justification = "<Pending>")]
    public sealed class Report
    {
        /// <summary>
        /// The ID of the report.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The name of the report.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The type of the report.
        /// </summary>
        [JsonProperty("type")]
        public ReportType Type { get; set; }

        /// <summary>
        /// The time interval when the report runs.
        /// </summary>
        /// <remarks>
        ///  Please mind that value 1 (instant report) is excluded from the valid options.
        /// </remarks>
        [JsonProperty("occurrence")]
        public ReportOccurrence Occurrence { get; set; }
    }
}
