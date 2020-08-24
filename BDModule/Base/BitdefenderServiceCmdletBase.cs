using System;
using System.Management.Automation;

namespace BDModule.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TParams"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class BitdefenderServiceCmdletBase<TParams, TResult> : BitdefenderCmdletBase<TParams, TResult>
        where TParams : class
    {
        /// <summary>
        /// Bitdefender API service name
        /// </summary>
        public abstract string Service { get; set; }

        /// <summary>
        /// Bitdefender API relative url with service name
        /// </summary>
        protected override string ApiUrl => $"{base.ApiUrl}/{Service.ToLower()}";

        internal BitdefenderServiceCmdletBase(string relativeUrl, string method) : base(relativeUrl, method)
        { /* NOP */}
    }
}
