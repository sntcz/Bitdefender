using System;

namespace BDModule.Models
{
    /// <summary>
    /// The report type
    /// </summary>
    public enum ReportType
    {
        /// <summary>
        /// <para type="description">1 - Antiphishing Activity</para>
        /// </summary>
        AntiphishingActivity = 1,
        /// <summary>
        /// <para type="description">2 - Blocked Applications</para>
        /// </summary>
        BlockedApplications = 2,
        /// <summary>
        /// <para type="description">3 - Blocked Websites</para>
        /// </summary>
        BlockedWebsites = 3,
        /// <summary>
        /// <para type="description">5 - Data Protection</para>
        /// </summary>
        DataProtection = 5,
        /// <summary>
        /// <para type="description">6 - Device Control Activity</para>
        /// </summary>
        DeviceControlActivity = 6,
        /// <summary>
        /// <para type="description">7 - Endpoint Modules Status</para>
        /// </summary>
        EndpointModulesStatus = 7,
        /// <summary>
        /// <para type="description">8 - Endpoint Protection Status</para>
        /// </summary>
        EndpointProtectionStatus = 8,
        /// <summary>
        /// <para type="description">9 - Firewall Activity</para>
        /// </summary>
        FirewallActivity = 9,
        /// <summary>
        /// <para type="description">12 - Malware Status</para>
        /// </summary>
        MalwareStatus = 12,
        /// <summary>
        /// <para type="description">14 - Network Status</para>
        /// </summary>
        NetworkStatus = 14,
        /// <summary>
        /// <para type="description">15 - On demand scanning</para>
        /// </summary>
        Ondemandscanning = 15,
        /// <summary>
        /// <para type="description">16 - Policy Compliance</para>
        /// </summary>
        PolicyCompliance = 16,
        /// <summary>
        /// <para type="description">17 - Security Audit</para>
        /// </summary>
        SecurityAudit = 17,
        /// <summary>
        /// <para type="description">18 - Security Server Status</para>
        /// </summary>
        SecurityServerStatus = 18,
        /// <summary>
        /// <para type="description">19 - Top 10 Detected Malware</para>
        /// </summary>
        Top10DetectedMalware = 19,
        /// <summary>
        /// <para type="description">21 - Top 10 Infected Endpoints</para>
        /// </summary>
        Top10InfectedEndpoints = 21,
        /// <summary>
        /// <para type="description">22 - Update Status</para>
        /// </summary>
        UpdateStatus = 22,
        /// <summary>
        /// <para type="description">25 - Virtual Machine Network Status</para>
        /// </summary>
        VirtualMachineNetworkStatus = 25,
        /// <summary>
        /// <para type="description">26 - HVI Activity</para>
        /// </summary>
        HVIActivity = 26,
        /// <summary>
        /// <para type="description">30 - Endpoint Encryption Status</para>
        /// </summary>
        EndpointEncryptionStatus = 30,
        /// <summary>
        /// <para type="description">31 - HyperDetect Activity</para>
        /// </summary>
        HyperDetectActivity = 31,
        /// <summary>
        /// <para type="description">32 - Network Patch Status</para>
        /// </summary>
        NetworkPatchStatus = 32,
        /// <summary>
        /// <para type="description">33 - Sandbox Analyzer Failed Submissions</para>
        /// </summary>
        SandboxAnalyzerFailedSubmissions = 33,
        /// <summary>
        /// <para type="description">34 - Network Incidents</para>
        /// </summary>
        NetworkIncidents = 34
    }
}
