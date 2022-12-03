using DoctorAppointment.Api;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.IntegrationTests
{
    [TestClass]
    public class UserControllerTests
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
        public async Task CreateUser_ReturnsCreated()
        {
            // arrange
            var client = _factory.CreateClient();

            var user = new RegisterModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@gmail.com",
                Role = "Doctor",
                Password = "123456.Abcde",
            };

            // act
            var response = await client.PostAsJsonAsync("/api/users/register", user);
            response.EnsureSuccessStatusCode();

            // assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public async Task GetUsers_ReturnsUsers()
        {
            // arrange
            var client = _factory.CreateClient();

            // act
            var response = await client.GetAsync("/api/users/get-users");
            response.EnsureSuccessStatusCode();
            var users = await response.Content.ReadFromJsonAsync<List<User>>();

            // assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        public async Task GetUserById_ReturnsUser()
        {
            // arrange
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/users/get-users");
            response.EnsureSuccessStatusCode();
            var users = await response.Content.ReadFromJsonAsync<List<User>>();

            // act
            var userResponse = await client.GetAsync($"/api/users/get-user/{users[0].Id}");
            userResponse.EnsureSuccessStatusCode();
            var user = await userResponse.Content.ReadFromJsonAsync<User>();

            // assert
            Assert.AreEqual(HttpStatusCode.OK, userResponse.StatusCode);
            Assert.AreEqual(users[0].Id, user.Id);
        }
    }
}
