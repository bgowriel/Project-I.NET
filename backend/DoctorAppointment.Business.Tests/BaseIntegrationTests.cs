using DoctorAppointment.Api.Controllers;
using DoctorAppointment.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;


namespace DoctorAppointment.Business.Tests
{
    public class BaseIntegrationTests
    {
        protected HttpClient HttpClient { get; private set; }
        protected BaseIntegrationTests()
        {
            var application = new WebApplicationFactory<AppointmentController>()
                .WithWebHostBuilder(builder => { });
            HttpClient = application.CreateClient();
            CleanDatabases();
        }

        private void CleanDatabases()
        {
            var databaseContext = new DatabaseContext();
            databaseContext.Appointments.RemoveRange(databaseContext.Appointments.ToList());
            databaseContext.SaveChanges();
        }
    }
}