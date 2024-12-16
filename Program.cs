using SigmaAssignment;
using Microsoft.EntityFrameworkCore;
using SigmaAssignment.Mappings;

var builder = WebApplication.CreateBuilder(args);

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
