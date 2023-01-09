using DoctorAppointment.Api.Exceptions;
using DoctorAppointment.Application;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.DataAccess;
using DoctorAppointment.DataAccess.Repositories;
using DoctorAppointment.Domain.Models;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Reflection;

namespace DoctorAppointment.Api
{
    public class DoctorAppointmentPresentationTest
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ReportApiVersions = true;
                o.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-version"),
                    new MediaTypeApiVersionReader("ver"));
            });

            var mediatR = Assembly.GetAssembly(typeof(AssemblyMarker));
            
            if (mediatR == null)
            {
                throw new MediatRException("MediatR assembly not found");
            }
            services.AddMediatR(mediatR);
            services.AddAutoMapper(typeof(DoctorAppointmentPresentation));
            services.AddFluentValidationAutoValidation();

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }        
    }
}
