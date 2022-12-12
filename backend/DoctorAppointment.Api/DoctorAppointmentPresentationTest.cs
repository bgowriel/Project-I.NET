using DoctorAppointment.Application;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.DataAccess;
using DoctorAppointment.DataAccess.Repositories;
using DoctorAppointment.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace DoctorAppointment.Api
{
    public class DoctorAppointmentPresentationTest
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var mediatR = Assembly.GetAssembly(typeof(AssemblyMarker));
            if (mediatR == null)
            {
				throw new ArgumentNullException("MediatR assembly not found");
			}
			services.AddMediatR(mediatR);
            services.AddAutoMapper(typeof(DoctorAppointmentPresentation));
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IMedicalVisitRepository, MedicalVisitRepository>();
            services.AddScoped<IOfficeRepository, OfficeRepository>();
            services.AddScoped<IAvailableDateRepository, AvailableDateRepository>();

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DatabaseContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }        
    }
}
