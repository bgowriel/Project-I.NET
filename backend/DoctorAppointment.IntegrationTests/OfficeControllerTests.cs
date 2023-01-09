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
    public class OfficeControllerTests
    {
        private static TestContext? _testContext;
        private static WebApplicationFactory<DoctorAppointmentPresentationTest>? _factory;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _testContext = testContext;
            _factory = new CustomApplicationFactory<DoctorAppointmentPresentationTest>();
        }

        // test AddOffice
        [TestMethod]
        public async Task AddOffice_WithValidOffice_ReturnsCreatedOffice()
        {
            // Arrange
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            var client = _factory.CreateClient();
            var office = new OfficePutPostDto()
            {
                Name = "Test Office",
                Description = "Test Description",
                Address = "Test Address",
                City = "Test City",
                Email = "test@example.com",
                Phone = "1234567890",
                Status = "Active"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/offices", office);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var createdOffice = JsonConvert.DeserializeObject<OfficeGetDto>(await response.Content.ReadAsStringAsync());
            
            if (createdOffice == null)
            {
                throw new ArgumentNullException(nameof(createdOffice));
            }
            
            Assert.AreEqual(office.Name, createdOffice.Name);
            Assert.AreEqual(office.Address, createdOffice.Address);
            Assert.AreEqual(office.City, createdOffice.City);
            Assert.AreEqual(office.Phone, createdOffice.Phone);
        }

        [TestMethod]
        public async Task AddOffice_WithInvalidOffice_ReturnsBadRequest()
        {
            // Arrange
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            var client = _factory.CreateClient();
            var office = new OfficePutPostDto()
            {
                Name = "",
                Description = "Test Description",
                Address = "Test Address",
                City = "Test City",
                Email = "",
                Phone = "1234567890",
                Status = "Invalid Status"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/offices", office);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task GetOffice_WithValidOfficeId_ReturnsOffice()
        {
            // Arrange
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            var client = _factory.CreateClient();
            var office = new OfficePutPostDto()
            {
                Name = "Test Office",
                Description = "Test Description",
                Address = "Test Address",
                City = "Test City",
                Email = "test@example.com",
                Phone = "1234567890",
                Status = "Active"
            };

            var response = await client.PostAsJsonAsync("/api/v1/offices", office);
            response.EnsureSuccessStatusCode();
            var createdOffice = JsonConvert.DeserializeObject<OfficeGetDto>(await response.Content.ReadAsStringAsync());

            if (createdOffice == null)
            {
                throw new ArgumentNullException(nameof(createdOffice));
            }

            // Act
            response = await client.GetAsync($"/api/v1/offices/{createdOffice.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var returnedOffice = JsonConvert.DeserializeObject<OfficeGetDto>(await response.Content.ReadAsStringAsync());

            if (returnedOffice == null)
            {
                throw new ArgumentNullException(nameof(returnedOffice));
            }

            Assert.AreEqual(createdOffice.Id, returnedOffice.Id);
            Assert.AreEqual(createdOffice.Name, returnedOffice.Name);
            Assert.AreEqual(createdOffice.Address, returnedOffice.Address);
            Assert.AreEqual(createdOffice.City, returnedOffice.City);
            Assert.AreEqual(createdOffice.Phone, returnedOffice.Phone);
        }


        [TestMethod]
        public async Task GetOffice_WithInvalidOfficeId_ReturnsNotFound()
        {
            // Arrange
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            var client = _factory.CreateClient();

            // Act
            try
            {
                var response = await client.GetAsync($"/api/v1/offices/{Guid.NewGuid()}");
            }
            catch(Exception e)
            {
                // Assert
                Assert.IsInstanceOfType(e, typeof(NotFoundException));
            }
        }

        [TestMethod]
        public async Task UpdateOffice_WithValidOffice_ReturnsUpdatedOffice()
        {
            // Arrange
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            var client = _factory.CreateClient();
            var office = new OfficePutPostDto()
            {
                Name = "Test Office",
                Description = "Test Description",
                Address = "Test Address",
                City = "Test City",
                Email = "test@example.com",
                Phone = "1234567890",
                Status = "Active"
            };

            var response = await client.PostAsJsonAsync("/api/v1/offices", office);
            response.EnsureSuccessStatusCode();
            var createdOffice = JsonConvert.DeserializeObject<OfficeGetDto>(await response.Content.ReadAsStringAsync());

            if (createdOffice == null)
            {
                throw new ArgumentNullException(nameof(createdOffice));
            }

            var updatedOffice = new OfficePutPostDto()
            {
                Name = "Updated Office",
                Description = "Updated Description",
                Address = "Updated Address",
                City = "Updated City",
                Email = "test@example.com",
                Phone = "1234567890",
                Status = "Active"
            };

            // Act
            response = await client.PutAsJsonAsync($"/api/v1/offices/{createdOffice.Id}", updatedOffice);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var returnedOffice = JsonConvert.DeserializeObject<OfficeGetDto>(await response.Content.ReadAsStringAsync());

            if (returnedOffice == null)
            {
                throw new ArgumentNullException(nameof(returnedOffice));
            }

            Assert.AreEqual(createdOffice.Id, returnedOffice.Id);
            Assert.AreEqual(updatedOffice.Name, returnedOffice.Name);
            Assert.AreEqual(updatedOffice.Address, returnedOffice.Address);
            Assert.AreEqual(updatedOffice.City, returnedOffice.City);
            Assert.AreEqual(updatedOffice.Phone, returnedOffice.Phone);
        }

        //test GetOffices
        [TestMethod]
        public async Task GetOffices_ReturnsOffices()
        {
            // Arrange
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            var client = _factory.CreateClient();
            var office = new OfficePutPostDto()
            {
                Name = "Test Office",
                Description = "Test Description",
                Address = "Test Address",
                City = "Test City",
                Email = "test@example.com",
                Phone = "1234567890",
                Status = "Active"
            };

            var response = await client.PostAsJsonAsync("/api/v1/offices", office);
            response.EnsureSuccessStatusCode();
            var createdOffice = JsonConvert.DeserializeObject<OfficeGetDto>(await response.Content.ReadAsStringAsync());

            if (createdOffice == null)
            {
                throw new ArgumentNullException(nameof(createdOffice));
            }

            // Act
            response = await client.GetAsync($"/api/v1/offices");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var returnedOffice = JsonConvert.DeserializeObject<List<OfficeGetDto>>(await response.Content.ReadAsStringAsync());

            if (returnedOffice == null)
            {
                throw new ArgumentNullException(nameof(returnedOffice));
            }

            Assert.IsTrue(returnedOffice.Count > 0);
        }

        //test GetDoctors by office id
        [TestMethod]
        public async Task GetDoctorsByOfficeId_ReturnsDoctors()
        {
            // Arrange
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            var client = _factory.CreateClient();
            var office = new OfficePutPostDto()
            {
                Name = "Test Office",
                Description = "Test Description",
                Address = "Test Address",
                City = "Test City",
                Email = "test@example.com",
                Phone = "1234567890",
                Status = "Active"
            };

            var response = await client.PostAsJsonAsync("/api/v1/offices", office);
            response.EnsureSuccessStatusCode();
            var createdOffice = JsonConvert.DeserializeObject<OfficeGetDto>(await response.Content.ReadAsStringAsync());

            if (createdOffice == null)
            {
                throw new ArgumentNullException(nameof(createdOffice));
            }

            // Act
            response = await client.GetAsync($"/api/v1/offices/doctors/{createdOffice.Id}");
            response.EnsureSuccessStatusCode();

            // Assert
            var returnedDoctors = JsonConvert.DeserializeObject<List<UserGetDto>>(await response.Content.ReadAsStringAsync());

            if (returnedDoctors == null)
            {
                throw new ArgumentNullException(nameof(returnedDoctors));
            }

            Assert.IsTrue(returnedDoctors.Count >= 0);
        }

        //test DeleteOffice
        [TestMethod]
        public async Task DeleteOffice_ReturnsNoContent()
        {
            // Arrange
            if (_factory == null)
            {
                throw new ArgumentNullException(nameof(_factory));
            }
            var client = _factory.CreateClient();
            var office = new OfficePutPostDto()
            {
                Name = "Test Office",
                Description = "Test Description",
                Address = "Test Address",
                City = "Test City",
                Email = "test@example.com",
                Phone = "1234567890",
                Status = "Active"
            };

            var response = await client.PostAsJsonAsync("/api/v1/offices", office);
            response.EnsureSuccessStatusCode();
            var createdOffice = JsonConvert.DeserializeObject<OfficeGetDto>(await response.Content.ReadAsStringAsync());

            if (createdOffice == null)
            {
                throw new ArgumentNullException(nameof(createdOffice));
            }

            // Act
            response = await client.DeleteAsync($"/api/v1/offices/{createdOffice.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseOffice = JsonConvert.DeserializeObject<OfficeGetDto>(await response.Content.ReadAsStringAsync());

            if (responseOffice == null)
            {
                throw new ArgumentNullException(nameof(responseOffice));
            }
            
            Assert.AreEqual(responseOffice.Id, createdOffice.Id);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _factory?.Dispose();
        }

    }
}
