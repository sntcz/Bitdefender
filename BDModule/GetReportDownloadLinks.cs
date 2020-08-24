using BDModule.Models;
using System;
using System.Management.Automation;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace BDModule
{
    /// <summary>
    /// <para type="synopsis">Get Bitdefender information regarding the report availability 
    /// for download and the corresponding download links.</para>
    /// <para type="description">The instant report is created one time only and available for download for less than 24 hours.</para>
    /// <para type="description">Scheduled reports are generated periodically and all report instances are saved in the GravityZone database.</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "BitdefenderReportDownloadLinks")]
    [OutputType(typeof(DownloadLinks))]
    public class GetReportDownloadLinks : Base.BitdefenderCmdletBase<Parameters.GetReportDownloadLinks, DownloadLinks>
    {

        /// <summary>
        /// Bitdefender API input parameters.
        /// </summary>
        protected override Parameters.GetReportDownloadLinks Parameters => new Parameters.GetReportDownloadLinks()
        {
            ReportId = ReportId
        };


        /// <summary>
        /// <para type="description">The ID of the report.</para>
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "The ID of the report.")]
        public string ReportId { get; set; }

        /// <summary>
        /// TBD
        /// </summary>
        public GetReportDownloadLinks() : base("reports", "getDownloadLinks")
        { /* NOP */ }

    }
}
