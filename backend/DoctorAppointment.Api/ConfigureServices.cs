using DoctorAppointment.Api.Exceptions;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using System.Reflection;

namespace DoctorAppointment.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assemblies = Assembly.GetAssembly(typeof(AssemblyMarker));
            if (assemblies == null)
            {
                throw new AssemblyException("MediatR assembly not found");
            }

            services.AddMediatR(assemblies);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
