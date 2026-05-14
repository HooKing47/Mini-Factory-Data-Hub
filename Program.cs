using FactoryDataApi.Data;
using FactoryDataApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// Register AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Auto-Migrate Database on Startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
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
    return await db.MachineRecords.ToListAsync();
});

app.Run();
