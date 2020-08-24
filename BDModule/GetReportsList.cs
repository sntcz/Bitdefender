using BDModule.Models;
using System;
using System.Management.Automation;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace BDModule
{
    /// <summary>
    /// <para type="synopsis">Get Bitdefender list of scheduled reports, according to the parameters received.</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "BitdefenderReportsList", SupportsPaging = true)]
    [OutputType(typeof(Report))]
    public class GetReportsList : Base.BitdefenderServiceCmdletBase<Parameters.GetReportsList, Report>
    {

        /// <summary>
        /// Bitdefender API input parameters.
        /// </summary>
        protected override Parameters.GetReportsList Parameters => new Parameters.GetReportsList()
        {
            Name = Name,
            Type = Type
        };

        /// <summary>
        /// <para type="description">This method requires you to place the {service} name in the API URL. The allowed services are:</para>
        /// <para type="description">computers, for "Computers and Virtual Machines"</para>
        /// <para type="description">virtualmachines, for "Virtual Machines"</para>
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "This method requires you to place the {service} name in the API URL.")]
        [ValidateSet("computers", "virtualmachines")]
        public override string Service { get; set; } = "computers";

        /// <summary>
        /// <para type="description">The name of the report.</para>
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "The name of the report.")]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">The report type.</para>
        /// </summary>
        [Parameter(Mandatory = true, HelpMessage = "The report type.")]
        public ReportType Type { get; set; }

        /// <summary>
        /// TBD
        /// </summary>
        public GetReportsList() : base("reports", "getReportsList")
        { /* NOP */ }

    }
}
