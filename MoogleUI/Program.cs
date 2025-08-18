using MoogleUI.Components;
using MoogleEngine;
using MoogleEngine.AppConfig;
using Shared;
namespace MoogleUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddRazorComponents();
            builder.Services.AddRazorComponents().AddInteractiveServerComponents();
            builder.Services.AddServerSideBlazor().AddCircuitOptions(options => {
                options.DetailedErrors = true;  // Enable detailed errors
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapBlazorHub();  // Essential for event handling
            app.MapRazorComponents<App>().AddInteractiveServerRenderMode(); ;


            app.Run();
        }
    }
}
