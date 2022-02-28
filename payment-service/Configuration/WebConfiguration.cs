// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace payment_service.Configuration
{
    public static class WebConfiguration
    {
        public static IServiceCollection AddWebModule(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddMvc();

            //https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-2.2
            services.AddHealthChecks();

          


            return services;
        }

        public static IApplicationBuilder UseApplicationWeb(this IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseHealthChecks("/health");


            return app;
        }

    }
}
