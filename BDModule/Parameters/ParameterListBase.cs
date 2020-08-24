using Newtonsoft.Json;
using System;

namespace BDModule.Parameters
{
    /// <summary>
    /// List input parameter base class.
    /// </summary>
    public class ParameterListBase
    {
        /// <summary>
        /// The results page number. Default page number is 1.
        /// </summary>
        [JsonProperty("page")]
        public int Page { get; set; } = 1;
        /// <summary>
        /// The number of items displayed in a page. The upper limit is 100 items per page.
        /// Default value: 30 items per page.
        /// </summary>
        [JsonProperty("perPage")]
        public int PerPage { get; set; } = 30;
    }
}