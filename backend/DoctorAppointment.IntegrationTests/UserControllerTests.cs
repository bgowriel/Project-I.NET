using DoctorAppointment.Api;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http.Json;

namespace DoctorAppointment.IntegrationTests
{
    [TestClass]
    public class UserControllerTests
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
        public async Task CreateUser_ReturnsCreated()
        {
            // arrange
			if (_factory == null)
            {
				throw new ArgumentNullException(nameof(_factory));
			}
            var client = _factory.CreateClient();

            var user = new RegisterModel
            {
                FirstName = "John",
                LastName = "Down",
                Email = "john.down@gmail.com",
                PhoneNumber = "1234567890",
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
           
			if (_factory == null)
			{
				throw new ArgumentNullException(nameof(_factory));
			}
			var client = _factory.CreateClient();
			// act
			var response = await client.GetAsync("/api/users");
            response.EnsureSuccessStatusCode();
            var users = await response.Content.ReadFromJsonAsync<List<User>>();
			
			if (users == null)
            {
				throw new ArgumentNullException(nameof(users));
			}

            // assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        public async Task GetUserById_ReturnsUser()
        {
            // arrange
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/users");
            response.EnsureSuccessStatusCode();
            var users = await response.Content.ReadFromJsonAsync<List<User>>();
			if (users == null)
			{
				throw new ArgumentNullException(nameof(users));
			}

			// act
			var userResponse = await client.GetAsync($"/api/users/{users[0].Id}");
            userResponse.EnsureSuccessStatusCode();
            var user = await userResponse.Content.ReadFromJsonAsync<User>();

			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			// assert
			Assert.AreEqual(HttpStatusCode.OK, userResponse.StatusCode);
            Assert.AreEqual(users[0].Id, user.Id);
        }

        [TestMethod]
        public async Task UpdateUser_ReturnsOk()
        {
            // arrange

            if (_factory == null)
            {
				throw new ArgumentNullException(nameof(_factory));
			}
			
			var client = _factory.CreateClient();
			
		
            var response = await client.GetAsync("/api/users");
            response.EnsureSuccessStatusCode();
            var users = await response.Content.ReadFromJsonAsync<List<User>>();

            if (users == null)
            {
				throw new ArgumentNullException(nameof(users));
			}

			users[0].Email = "john.noname@example.com";

            // act
            var userResponse = await client.PutAsJsonAsync($"/api/users/update-user", users[0]);
            userResponse.EnsureSuccessStatusCode();

            // assert
            Assert.AreEqual(HttpStatusCode.OK, userResponse.StatusCode);
        }

        [TestMethod]
        public async Task DeleteUser_ReturnsOk()
        {
            // arrange

            if (_factory == null)
            {
				throw new ArgumentNullException(nameof(_factory));
			}
			

			var client = _factory.CreateClient();
            // create a new user
            var user = new RegisterModel
            {
                FirstName = "John",
                LastName = "Down",
                Email = "john.downridge@example.com",
                PhoneNumber = "1234567890",
                Role = "Doctor",
                Password = "123456.Abcde",
            };

            var response = await client.PostAsJsonAsync("/api/users/register", user);
            response.EnsureSuccessStatusCode();
            // get the new user id
            var newUser = await response.Content.ReadFromJsonAsync<User>();
            if (newUser == null)
            {
				throw new ArgumentNullException(nameof(newUser));
			}
			// act
			var userResponse = await client.DeleteAsync($"/api/users/{newUser.Id}");
            userResponse.EnsureSuccessStatusCode();

            // assert
            Assert.AreEqual(HttpStatusCode.OK, userResponse.StatusCode);
        }
    }
        
}
