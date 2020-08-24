using Newtonsoft.Json;
using System;

namespace BDModule.Parameters
{
    /// <summary>
    /// Return list output parameter base class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReturnList<T>
    {
        /// <summary>
        /// The current page displayed.
        /// </summary>
        [JsonProperty("page")]
        public int Page { get; set; }
        /// <summary>
        /// The  total number of available pages.
        /// </summary>
        [JsonProperty("pagesCount")]
        public int PagesCount { get; set; }
        /// <summary>
        /// The  total number of returned items per page.
        /// </summary>
        [JsonProperty("perPage")]
        public int PerPage { get; set; }
        /// <summary>
        /// Items
        /// </summary>
        [JsonProperty("items")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "<Pending>")]
        public T[] Items { get; set; }
        /// <summary>
        /// The total number of items.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

    }
}
