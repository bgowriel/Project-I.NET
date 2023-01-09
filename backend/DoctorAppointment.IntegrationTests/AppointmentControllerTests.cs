using DoctorAppointment.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Json;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Api;
using DoctorAppointment.Application.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

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

            var usersResponse = await client.GetAsync("/api/v1/users");
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
            var response = await client.PostAsJsonAsync("/api/v1/appointments", appointment);
            response.EnsureSuccessStatusCode();

            // assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public async Task CreateAppointment_InvalidAppointment_ThrowsException()
        {
            // arrange
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            var client = _factory.CreateClient();

            var usersResponse = await client.GetAsync("/api/v1/users");
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
                Status = "",
                DoctorId = "",
                PatientId = users[1].Id,
                OfficeId = Guid.NewGuid()
            };

            // act
            try
            {
                var response = await client.PostAsJsonAsync("/api/v1/appointments", appointment);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // assert
                Assert.IsInstanceOfType(ex, typeof(DbUpdateException));
            }
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

            var response = await client.GetAsync("/api/v1/appointments");
            response.EnsureSuccessStatusCode();
            var appointments = JsonConvert.DeserializeObject<List<Appointment>>(await response.Content.ReadAsStringAsync());

            if (appointments == null)
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

            var appointmentId = appointments.FirstOrDefault()?.Id;

            if (appointmentId == null)
            {
                Assert.Fail("No appointments in database");
            }

            // act
            var appointmentResponse = await client.GetAsync($"/api/v1/appointments/{appointmentId}");
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
            try
            {
                var response = await client.GetAsync("/api/v1/appointments/00000000-0000-0000-0000-000000000002");
            }
            catch (Exception e)
            {
                // assert
                Assert.IsInstanceOfType(e, typeof(NotFoundException));
            }
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
            var response = await client.GetAsync("/api/v1/appointments");
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

            var usersResponse = await client.GetAsync("/api/v1/users");
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

            var doctorId = users.Where(x => x.Role == "Doctor").FirstOrDefault()?.Id;

            if (doctorId == null)
            {
                Assert.Fail("No doctors in database");
            }

            var patientId = users.Where(x => x.Role == "Patient").FirstOrDefault()?.Id;

            if (patientId == null)
            {
                Assert.Fail("No patients in database");
            }

            var response = await client.GetAsync("/api/v1/appointments/");
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
            var appointmentId = appointments.FirstOrDefault()?.Id;

            if (appointmentId == null)
            {
                Assert.Fail("No appointments in database");
            }

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
            var appointmentResponse = await client.PutAsJsonAsync($"/api/v1/appointments/{appointmentId}", appointment);
            appointmentResponse.EnsureSuccessStatusCode();

            // assert
            Assert.AreEqual(HttpStatusCode.OK, appointmentResponse.StatusCode);
        }

        [TestMethod]
        public async Task UpdateAppointment_ReturnsNotFound()
        {
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            // arrange
            var client = _factory.CreateClient();

            var usersResponse = await client.GetAsync("/api/v1/users");
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

            var doctorId = users.Where(x => x.Role == "Doctor").FirstOrDefault()?.Id;


            if (doctorId == null)
            {
                Assert.Fail("No doctors in database");
            }

            var patientId = users.Where(x => x.Role == "Patient").FirstOrDefault()?.Id;


            if (patientId == null)
            {
                Assert.Fail("No patients in database");
            }

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
            try
            {
                var appointmentResponse = await client.PutAsJsonAsync($"/api/v1/appointments/00000000-0000-0000-0000-000000000000", appointment);
            }
            catch (Exception e)
            {
                // assert
                Assert.AreEqual(HttpStatusCode.NotFound, e.Message);
            }
        }

        [TestMethod]
        public async Task GetAppointmentsByDate_ReturnsAppointments()
        {
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            // arrange
            var client = _factory.CreateClient();
            var date = Utilities.TestAppointment?.Date;
            var response = await client.GetAsync($"/api/v1/appointments/date/{date}");
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

            Assert.IsTrue(appointments.Count >= 0);
        }

        [TestMethod]
        public async Task GetAppointmentsByDoctor_ReturnsAppointments()
        {
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            // arrange
            var client = _factory.CreateClient();
            var doctorId = Utilities.TestAppointment?.DoctorId;
            var response = await client.GetAsync($"/api/v1/appointments/doctor/{doctorId}");
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

            Assert.IsTrue(appointments.Count >= 0);
        }

        [TestMethod]
        public async Task GetAppointmentsByPatient_ReturnsAppointments()
        {
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            // arrange
            var client = _factory.CreateClient();
            var patientId = Utilities.TestAppointment?.PatientId;
            var response = await client.GetAsync($"/api/v1/appointments/patient/{patientId}");
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

            Assert.IsTrue(appointments.Count >= 0);
        }

        // test DeleteAppointment
        [TestMethod]
        public async Task DeleteAppointment_ReturnsNoContent()
        {
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            // arrange
            var client = _factory.CreateClient();
            var appointmentId = Utilities.TestAppointment?.Id;
            var response = await client.DeleteAsync($"/api/v1/appointments/{appointmentId}");
            response.EnsureSuccessStatusCode();

            // assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task z_DeleteAppointment_ReturnsNullAppointment()
        {
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            // arrange
            var client = _factory.CreateClient();
            var appointmentId = Utilities.TestAppointment?.Id;
            var response = await client.DeleteAsync($"/api/v1/appointments/{appointmentId}");
            var appointment = JsonConvert.DeserializeObject<Appointment>(await response.Content.ReadAsStringAsync());
            
            if(appointment == null)
            {
                Assert.Fail("Appointment not found");
            }

            // assert
            Assert.AreEqual(Guid.Empty, appointment.Id);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _factory?.Dispose();
        }
    }
}
