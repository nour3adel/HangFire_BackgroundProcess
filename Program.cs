using Hangfire;
using HangfireExample;
using HangfireExample.Models;
using HangfireExample.Services;
using HangfireExample.Settings;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Add your connection string in appsettings.json

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));
builder.Services.AddHangfireServer();
builder.Services.AddControllers();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<EmailService>();
builder.Services.AddTransient<ReportGenerator>();
builder.Services.AddTransient<YourJob>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Hangfire Dashboard
app.UseHangfireDashboard(); // This sets up the dashboard at /hangfire by default
app.UseHangfireServer(); // No parameters needed

// Map controller routes
app.MapControllers();

// Run the application
app.Run();

