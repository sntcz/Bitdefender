using Newtonsoft.Json;
using System;

namespace BDModule.JsonRpc
{
    /// <summary>
    /// JSON-RPC 2.0 response model.
    /// </summary>
    /// <typeparam name="TResult">Type of the Result object.</typeparam>
    internal sealed class JsonRpcResponse<TResult>
    {
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("jsonrpc", Required = Required.Always)]
        public string Version { get; set; }

        [JsonProperty("result")]
        public TResult Result { get; set; }

        [JsonProperty("error")]
        public JsonRpcError Error { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
