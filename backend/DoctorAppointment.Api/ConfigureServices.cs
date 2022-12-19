using DoctorAppointment.Api;
using DoctorAppointment.Api.Exceptions;
using DoctorAppointment.Application.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
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
            //services.AddAutoMapper(typeof(DoctorAppointmentPresentation));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(DoctorAppointmentPresentation)));
            return services;
        }
    }
}
