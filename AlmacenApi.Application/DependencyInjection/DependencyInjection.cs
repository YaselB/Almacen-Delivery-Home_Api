using System.Reflection;
using AlmacenApi.Domain.Common.Interfaces.Repository.AdminRepo;
using AlmacenApi.Domain.Repository.Generic;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AlmacenApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Registrar MediatR
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            
            // Registrar AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            return services;
        }
    }
}