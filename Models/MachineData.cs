using System;

namespace FactoryDataApi.Models
{
    public class MachineData
    {
        public int Id { get; set; }
        public string MachineId { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}