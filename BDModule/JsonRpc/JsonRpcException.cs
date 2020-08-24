using System;

namespace BDModule.JsonRpc
{
    /// <summary>
    /// JSON-RPC 2.0 response error.
    /// </summary>
    [Serializable]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "<Pending>")]
    public sealed class JsonRpcException : Exception
    {
        /// <summary>
        /// JSON-RPC 2.0 error code.
        /// </summary>
        public int Code => (int)Data["Code"];

        internal JsonRpcException(JsonRpcError error)
            : base(error.ToString())
        {
            Data.Add("Code", error.Code);
            Data.Add("Message", error.Message);
            foreach (var p in error?.Data)
            {
                Data.Add(p.Key, p.Value);
            }
        }

        private JsonRpcException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        { /* NOP */ }
    }
}
