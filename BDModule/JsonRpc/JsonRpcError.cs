using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BDModule.JsonRpc
{
    internal sealed class JsonRpcError
    {
        [JsonProperty("code", Required = Required.Always)]
        public int Code { get; set; }

        [JsonProperty("message", Required = Required.Always)]
        public string Message { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, string> Data { get; set; }


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Code.ToString("D", CultureInfo.InvariantCulture));
            sb.Append(": ");
            sb.Append(Message);

            if ((Data == null) || (Data.Count == 0))
            {
                return sb.ToString();
            }

            foreach (var p in Data)
            {
                sb.AppendLine();
                sb.Append("  ");
                sb.Append(p.Key);
                sb.Append(": ");
                sb.Append(p.Value);
            }

            return sb.ToString();
        }
    }
}
