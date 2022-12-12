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
        private static TestContext? _testContext;
        private static WebApplicationFactory<DoctorAppointmentPresentationTest>? _factory;
        

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
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            var client = _factory.CreateClient();

            var usersResponse = await client.GetAsync("/api/users");
            usersResponse.EnsureSuccessStatusCode();
            var users = JsonConvert.DeserializeObject<List<User>>(await usersResponse.Content.ReadAsStringAsync());

            if (users == null)
            {
				Assert.Fail("No users found");
			}
			
			if (users.Count == 0)
			{
				Assert.Inconclusive("No users in database");
			}

			if (users[0] == null || users[1] == null)
			{
				Assert.Fail("No users in database");
			}

			var appointment = new AppointmentPutPostDto
            {
                Date = DateTime.Now,
                Description = "Surgeon",
                Status = "Pending",
                DoctorId = users[0].Id,
                PatientId = users[1].Id,
                OfficeId = Guid.NewGuid()
            };
            
            // act
            var response = await client.PostAsJsonAsync("/api/appointments", appointment);
            response.EnsureSuccessStatusCode();
            
            // assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public async Task GetAppointmentById_ReturnsAppointment()
        {
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            // arrange
            var client = _factory.CreateClient();
            
            var response = await client.GetAsync("/api/appointments");
            response.EnsureSuccessStatusCode();
            var appointments = JsonConvert.DeserializeObject<List<Appointment>>(await response.Content.ReadAsStringAsync());

			if(appointments == null)
            {
				Assert.Fail("No appointments found");
			}
			
			if (appointments.Count == 0)
			{
				Assert.Inconclusive("No appointments in database");
			}

            if (appointments.FirstOrDefault() == null)
            {
                Assert.Fail("No appointments in database");
            }

            if (appointments.FirstOrDefault()?.Id == null)
            {
                Assert.Fail("No appointments in database");
            }
    
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

			if (appointment == null)
			{
				Assert.Fail("No appointment found");
			}
			
			Assert.AreEqual(appointmentId, appointment.Id);
        }

        [TestMethod]
        public async Task GetAppointmentById_ReturnsNotFound()
        {
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            // arrange
            var client = _factory.CreateClient();

            // act
            var response = await client.GetAsync("/api/appointments/00000000-0000-0000-0000-000000000002");

            // assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task GetAppointments_ReturnsAppointments()
        {
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            // arrange
            var client = _factory.CreateClient();

            // act
            var response = await client.GetAsync("/api/appointments");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // assert
            if (string.IsNullOrWhiteSpace(responseString) == true)
            {
                Assert.Fail("Response is null");
            }
            var appointments = JsonConvert.DeserializeObject<List<Appointment>>(responseString);

			if (appointments == null)
			{
				Assert.Fail("No appointments found");
			}

			if (appointments.Count == 0)
			{
				Assert.Inconclusive("No appointments in database");
			}

			Assert.IsTrue(appointments.Count > 0);
        }

        [TestMethod]
        public async Task UpdateAppointment_ReturnsOk()
        {
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            // arrange
            var client = _factory.CreateClient();
            
            var usersResponse = await client.GetAsync("/api/users");
            usersResponse.EnsureSuccessStatusCode();
            var users = JsonConvert.DeserializeObject<List<User>>(await usersResponse.Content.ReadAsStringAsync());
			if (users == null)
			{
				Assert.Fail("No users found");
			}

			if (users.Count == 0)
			{
				Assert.Inconclusive("No users in database");
			}

            if (users.Where(x => x.Role == "Doctor").FirstOrDefault() == null)
            {
                Assert.Fail("No doctors in database");
            }

            if (users.Where(x => x.Role == "Doctor").FirstOrDefault()?.Id == null)
            {
                Assert.Fail("No doctors in database");
            }

            if (users.Where(x => x.Role == "Patient").FirstOrDefault() == null)
            {
                Assert.Fail("No patients in database");
            }

            if (users.Where(x => x.Role == "Patient").FirstOrDefault()?.Id == null)
            {
                Assert.Fail("No patients in database");
            }


            var doctorId = users.Where(x => x.Role == "Doctor").FirstOrDefault().Id;
            var patientId = users.Where(x => x.Role == "Patient").FirstOrDefault().Id;

            var response = await client.GetAsync("/api/appointments/");
            response.EnsureSuccessStatusCode();
            var appointments = JsonConvert.DeserializeObject<List<Appointment>>(await response.Content.ReadAsStringAsync());
            if (appointments == null)
            {
                Assert.Fail("No appointments found");
            }
            
            if (appointments.FirstOrDefault() == null)
            {
                Assert.Fail("No appointments in database");
            }

            if (appointments.FirstOrDefault()?.Id == null)
            {
                Assert.Fail("No appointments in database");
            }
            var appointmentId = appointments.FirstOrDefault().Id;

            var appointment = new AppointmentPutPostDto
            {
                Date = DateTime.Now,
                Description = "Surgeon",
                Status = "Pending",
                DoctorId = doctorId,
                PatientId = patientId,
                OfficeId = Guid.NewGuid()
            };

            // act
            var appointmentResponse = await client.PutAsJsonAsync($"/api/appointments/{appointmentId}", appointment);
            appointmentResponse.EnsureSuccessStatusCode();

            // assert
            Assert.AreEqual(HttpStatusCode.OK, appointmentResponse.StatusCode);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _factory?.Dispose();
        }
    }
}
