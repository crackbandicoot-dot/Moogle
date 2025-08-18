using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Shared;
using MoogleEngine;
using MoogleEngine.AppConfig;

// Add this using directive to reference the MainLayout in UIComponents

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Register MainLayout as a scoped service if you need to inject it (optional)
// builder.Services.AddScoped<MainLayout>();

builder.Services.AddScoped<ISearchService, Moogle>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/UIComponents/MainLayout.razor");

app.Run();
