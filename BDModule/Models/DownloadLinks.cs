using Newtonsoft.Json;
using System;

namespace BDModule.Models
{
    /// <summary>
    /// TBD
    /// </summary>
    [Serializable]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields", Justification = "<Pending>")]
    public sealed class DownloadLinks
    {
        /// <summary>
        /// True if the report is ready to be downloaded or False otherwise.
        /// </summary>
        [JsonProperty("readyForDownload")]
        public bool ReadyForDownload { get; set; }
        /// <summary>
        /// The URL for downloading the last instance of an
        /// instant or scheduled report.It will be present in the response only if
        /// readyForDownload is True.The downloaded result is an archive with two
        /// files: a CSV and a PDF.Both files refer to the same last instance of the report.
        /// </summary>
        [JsonProperty("lastInstanceUrl")]
        public string LastInstanceUrl { get; set; }
        /// <summary>
        /// The URL downloads an archive with all generated
        /// instances of the scheduled report.The field will be present in the response only
        /// if readyForDownload is True and the report is a scheduled one.The
        /// downloaded result is an archive with a pair of files for each instance of the
        /// report: a CSV and a PDF file.Both files refer to the same instance of the report.
        /// </summary>
        [JsonProperty("allInstancesUrl")]
        public string AllInstancesUrl { get; set; }
    }
}
