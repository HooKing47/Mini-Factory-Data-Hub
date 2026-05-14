using FactoryDataApi.Data;
using FactoryDataApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// ปลดล็อก CORS ให้หน้าเว็บสามารถเรียกใช้ API ได้
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Register AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()));

var app = builder.Build();

// Auto-Migrate Database on Startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseCors("AllowAll");
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();

// POST: api/machinedata
app.MapPost("/api/machinedata", async (MachineData data, AppDbContext db) =>
{
    data.Timestamp = DateTime.UtcNow;
    db.MachineRecords.Add(data);
    await db.SaveChangesAsync();
    return Results.Created($"/api/machinedata/{data.Id}", data);
});

// GET: api/machinedata
app.MapGet("/api/machinedata", async (AppDbContext db) =>
{
    // ดึงเฉพาะ 50 รายการล่าสุด เพื่อไม่ให้รกเกินไป
    return await db.MachineRecords.OrderByDescending(m => m.Timestamp).Take(50).ToListAsync();
});

// POST: api/maintenancerequests (แจ้งซ่อม)
app.MapPost("/api/maintenancerequests", async (MaintenanceRequest req, AppDbContext db) =>
{
    req.ReportedAt = DateTime.UtcNow;
    req.IsResolved = false;
    db.MaintenanceRequests.Add(req);
    await db.SaveChangesAsync();
    return Results.Created($"/api/maintenancerequests/{req.Id}", req);
});

// GET: api/maintenancerequests (ดึงรายการแจ้งซ่อมทั้งหมด)
app.MapGet("/api/maintenancerequests", async (AppDbContext db) =>
{
    return await db.MaintenanceRequests.OrderByDescending(m => m.ReportedAt).ToListAsync();
});

app.Run();
