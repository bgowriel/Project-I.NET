using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorAppointment.DataAccess
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IMedicalVisitRepository, MedicalVisitRepository>();
            services.AddScoped<IOfficeRepository, OfficeRepository>();
            services.AddScoped<IAvailableDateRepository, AvailableDateRepository>();

            services.AddDbContext<DatabaseContext>(optionsAction: options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
