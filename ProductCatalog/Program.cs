using Microsoft.EntityFrameworkCore;
using ProductCatalogInfrastructure.Data;
using Microsoft.Extensions.Logging;
using System.Configuration;
using ProductCatalog;
using ProductCatalog.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbAndIdentityConfig(builder.Configuration);

// Core Service Config
builder.Services.AddCoreScopedConfig();

builder.Services.AddMiscConfig();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
