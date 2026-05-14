using FactoryDataApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FactoryDataApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<MachineData> MachineRecords { get; set; }
        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
    }
}