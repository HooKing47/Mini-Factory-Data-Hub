using System;

namespace FactoryDataApi.Models
{
    public class MaintenanceRequest
    {
        public int Id { get; set; }
        public string MachineId { get; set; } = string.Empty;
        public string IssueDescription { get; set; } = string.Empty;
        public string ReportedBy { get; set; } = string.Empty;
        public DateTime ReportedAt { get; set; }
        public bool IsResolved { get; set; }
        public string Priority { get; set;} = "Normal";
    }
}
