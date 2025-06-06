﻿using Microsoft.AspNetCore.Builder;
using System.Runtime.CompilerServices;

namespace Talabat.APIs.Extensions
{
    public static class SwaggerServicesExtension
    {

        public static IServiceCollection AddSwaggerServices( this IServiceCollection services )
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }  

        public static WebApplication  UseSwaggerMiddlewares(this WebApplication app)
        {

            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }      
}
