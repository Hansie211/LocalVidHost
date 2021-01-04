using BlazorApp.Connection.Client;
using BlazorApp.Connection.Server;
using BlazorApp.Hubs;
using Catalogus.Movies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddScoped<MovieCatalogus>();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler( "/Error" );
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            Trace.WriteLine( $"Movie path: { SettingsLib.MainSettings.Instance.MoviePath }" );

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStaticFiles( new StaticFileOptions() {
                FileProvider = new PhysicalFileProvider( SettingsLib.MainSettings.Instance.MoviePath ),
                RequestPath =  "/Storage",
            } );

            app.UseRouting();

            app.UseEndpoints( endpoints => {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage( "/_Host" );
                endpoints.MapHub<VideoServerActionHub>( "/serverActionHub" );
                endpoints.MapHub<VideoClientActionHub>( "/clientActionHub" );
            } );
        }
    }
}
