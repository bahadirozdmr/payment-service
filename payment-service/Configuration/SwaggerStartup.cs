// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace payment_service.Configuration
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerModule(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Payment API", Version = "0.0.1" });
            });

            return services;
        }

        public static IApplicationBuilder UseApplicationSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "{documentName}/api-docs";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/v2/api-docs", "Payment API");
            });
            return app;
        }
    }
}
