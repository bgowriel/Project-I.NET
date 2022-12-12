using AutoMapper;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Api.Controllers;
using DoctorAppointment.Application.Commands;
using DoctorAppointment.Domain.Models;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using DoctorAppointment.Application.Queries;

namespace DoctorAppointment.UnitTests
{
    [TestFixture]
    public class UserControllerFixture
    {
        private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
        private Mock<IMapper> _mockMapper;
        private Mock<IConfiguration> _mockConfiguration = new Mock<IConfiguration>();
        private Mock<RoleManager<IdentityRole>> _mockRoleManager = new Mock<RoleManager<IdentityRole>>(Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);
        private Mock<UserManager<User>> _mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        private User _user;
        private UserGetDto _userGetDto;
        //RegisterModel is the UserPutPostDto
        private RegisterModel _registerModel;

        [SetUp]
        public void Setup()
        {
            _registerModel = new RegisterModel
            {
                FirstName = "Carl",
                LastName = "Testito",
                Role = "Patient",
                Email = "carl.testito@example.com",
                PhoneNumber = "01298319024721",
                Password = "CarlTestito.2022"
            };

            _user = new User
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Carl",
                LastName = "Testito",
                Role = "Patient",
                Email = "carl.testito@example.com",
                UserName = "carl.testito@example.com"
            };

            _userGetDto = new UserGetDto
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Carl",
                LastName = "Testito",
                Role = "Patient",
                Email = "carl.testito@example.com",
                PhoneNumber = "01298319024721"
            };

            _mockMapper = MappingData();
        }

        private Mock<IMapper> MappingData()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterModel, User>();
                cfg.CreateMap<User, UserGetDto>();
            });

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<User>(It.IsAny<RegisterModel>())).Returns(_user);
            mockMapper.Setup(m => m.Map<UserGetDto>(It.IsAny<User>())).Returns(_userGetDto);

            return mockMapper;
        }

        [Test]
        public async Task PostUser_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<InsertUser>(), It.IsAny<CancellationToken>())).ReturnsAsync(_user);

            var controller = new UsersController(_mockConfiguration.Object, _mockUserManager.Object, _mockRoleManager.Object, _mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.Register(_registerModel);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public async Task PostUser_WhenCalled_ReturnsCorrectItem()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<InsertUser>(), It.IsAny<CancellationToken>())).ReturnsAsync(_user);

            var controller = new UsersController(_mockConfiguration.Object, _mockUserManager.Object, _mockRoleManager.Object, _mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.Register(_registerModel) as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Value, Is.EqualTo(_userGetDto));
        }

        [Test]
        public async Task PostUser_WhenCalled_ReturnsBadRequest()
        {
            _registerModel.Password = "x";
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<InsertUser>(), It.IsAny<CancellationToken>())).ReturnsAsync(_user);

            var controller = new UsersController(_mockConfiguration.Object, _mockUserManager.Object, _mockRoleManager.Object, _mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.Register(_registerModel) as BadRequestObjectResult;

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task PostUser_WhenCalled_ReturnsBadRequestMessage()
        {
            _registerModel.Email = null;
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<InsertUser>(), It.IsAny<CancellationToken>())).ReturnsAsync(_user);

            var controller = new UsersController(_mockConfiguration.Object, _mockUserManager.Object, _mockRoleManager.Object, _mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.Register(_registerModel) as BadRequestObjectResult;

            // Assert
            Assert.That(result.Value.ToString(), Is.EqualTo("{ message = Email is required }"));
        }

        [Test]
        public async Task GetUser_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetUserById>(), It.IsAny<CancellationToken>())).ReturnsAsync(_user);

            var controller = new UsersController(_mockConfiguration.Object, _mockUserManager.Object, _mockRoleManager.Object, _mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.GetUser(_user.Id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetUser_WhenCalled_ReturnsCorrectItem()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetUserById>(), It.IsAny<CancellationToken>())).ReturnsAsync(_user);

            var controller = new UsersController(_mockConfiguration.Object, _mockUserManager.Object, _mockRoleManager.Object, _mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.GetUser(_user.Id) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Value, Is.EqualTo(_userGetDto));
        }

        [Test]
        public async Task GetUser_WhenCalled_ReturnsNotFound()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetUserById>(), It.IsAny<CancellationToken>())).ReturnsAsync(new User());

            var controller = new UsersController(_mockConfiguration.Object, _mockUserManager.Object, _mockRoleManager.Object, _mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.GetUser(new User().Id);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
