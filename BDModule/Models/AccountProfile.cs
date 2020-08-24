using Newtonsoft.Json;
using System;

namespace BDModule.Models
{
    /// <summary>
    /// The profile information of the user account containing: fullName, timezone and language.
    /// </summary>
    [Serializable]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields", Justification = "<Pending>")]
    public sealed class AccountProfile
    {
        /// <summary>
        /// Full name
        /// </summary>
        [JsonProperty("fullName")]
        public string FullName { get; set; }
        /// <summary>
        /// Language
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }
        /// <summary>
        /// Timezone
        /// </summary>
        [JsonProperty("timezone")]
        public string Timezone { get; set; }
    }
}
