using DoctorAppointment.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Json;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Api;

namespace DoctorAppointment.IntegrationTests
{
    [TestClass]
    public class AppointmentControllerTests
    {
        private static TestContext _testContext;
        private static WebApplicationFactory<DoctorAppointmentPresentationTest> _factory;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _testContext = testContext;
            _factory = new CustomApplicationFactory<DoctorAppointmentPresentationTest>();
        }

        [TestMethod]
        public async Task CreateAppointment_ReturnsCreated()
        {
            // arrange
            var client = _factory.CreateClient();

            var usersResponse = await client.GetAsync("/api/users/get-users");
            usersResponse.EnsureSuccessStatusCode();
            var users = JsonConvert.DeserializeObject<List<User>>(await usersResponse.Content.ReadAsStringAsync());

            var appointment = new AppointmentPutPostDto
            {
                Date = DateTime.Now,
                Description = "Surgeon",
                Status = "Pending",
                DoctorId = users[0].Id,
                PatientId = users[1].Id
            };
            
            // act
            var response = await client.PostAsJsonAsync("/api/appointments/", appointment);
            response.EnsureSuccessStatusCode();
            
            // assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        //[TestMethod]
        public async Task GetAppointmentById_ReturnsAppointment()
        {
            // arrange
            var client = _factory.CreateClient();
            
            var response = await client.GetAsync("/api/appointments/");
            response.EnsureSuccessStatusCode();
            var appointments = JsonConvert.DeserializeObject<List<Appointment>>(await response.Content.ReadAsStringAsync());
            var appointmentId = appointments.FirstOrDefault().Id;

            // act
            var appointmentResponse = await client.GetAsync($"/api/appointments/{appointmentId}");
            appointmentResponse.EnsureSuccessStatusCode();
            var responseString = await appointmentResponse.Content.ReadAsStringAsync();
            
            // assert
            if (string.IsNullOrWhiteSpace(responseString) == true)
            {
                Assert.Fail("Response is null");
            }
            var appointment = JsonConvert.DeserializeObject<Appointment>(responseString);
            Assert.AreEqual(appointmentId, appointment.Id.ToString());
        }

        [TestMethod]
        public async Task GetAppointmentById_ReturnsNotFound()
        {
            // arrange
            var client = _factory.CreateClient();

            // act
            var response = await client.GetAsync("/api/appointment/00000000-0000-0000-0000-000000000002");

            // assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _factory.Dispose();
        }
    }
}
