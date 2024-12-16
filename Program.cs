using SigmaAssignment;
using Microsoft.EntityFrameworkCore;
using SigmaAssignment.Mappings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add logging using serilog
builder.Host.UseSerilog((context, services, configuration) =>

configuration
    .ReadFrom.Configuration(context.Configuration)      // Optionally, read from appsettings.json
    .WriteTo.Console()                                 // Log to console
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)  // Log to file (daily rolling logs)
);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

//manage dependency injection
builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapControllers();


app.Run();