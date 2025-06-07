using Microsoft.EntityFrameworkCore;
using PersonalSite.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // Connection string will be in appsettings.json

builder.Services.AddControllers();
builder.Services.AddRazorPages(); // Added for Razor Pages

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Added for wwwroot content
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages(); // Added for Razor Pages
app.Run();
