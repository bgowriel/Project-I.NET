using AutoMapper;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Api.Controllers;
using DoctorAppointment.Application.Commands;
using DoctorAppointment.Domain.Models;
using MediatR;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using DoctorAppointment.Application.Queries;
using System.IdentityModel.Tokens.Jwt;

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
        private RegisterModel _registerModel;
        private LoginModel _loginModel;
        private JwtSecurityToken _jwtSecurityToken;

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

            _loginModel = new LoginModel
            {
                Email = "carl.testito@example.com",
                Password = "CarlTestito.2022"
            };

            _jwtSecurityToken = new JwtSecurityToken();

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
        public async Task _PostUser_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<InsertUser>(), It.IsAny<CancellationToken>())).ReturnsAsync(_user);

            var controller = new UsersController(_mockConfiguration.Object, _mockUserManager.Object, _mockRoleManager.Object, _mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.Register(_registerModel);

            // Assert
            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
        }

        // test login with _user
        [Test]
        public async Task LoginUserReturnsBadRequest()
        {
            // Arrange
            _mockUserManager.Setup(m => m.FindByNameAsync("carl.testito@example.com")).ReturnsAsync(_user);
            _mockUserManager.Setup(m => m.CheckPasswordAsync(_user, "CarlTestito.2022")).ReturnsAsync(true);
            _mockUserManager.Setup(m => m.GetRolesAsync(_user)).ReturnsAsync(new List<string> { "Patient" });

            var controller = new UsersController(_mockConfiguration.Object, _mockUserManager.Object, _mockRoleManager.Object, _mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.Login(_loginModel);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task _PostUser_WhenCalled_ReturnsCorrectItem()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<InsertUser>(), It.IsAny<CancellationToken>())).ReturnsAsync(_user);

            var controller = new UsersController(_mockConfiguration.Object, _mockUserManager.Object, _mockRoleManager.Object, _mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.Register(_registerModel) as CreatedAtActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
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
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
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
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.Not.Null);
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
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
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
            Assert.That(result, Is.Not.Null);
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
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        //test login
        [Test]
        public async Task Login_WhenCalled_ReturnsUnauthorized()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<LoginModel>(), It.IsAny<CancellationToken>())).ReturnsAsync(new User());

            var controller = new UsersController(_mockConfiguration.Object, _mockUserManager.Object, _mockRoleManager.Object, _mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.Login(_loginModel);

            // Assert
            Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
        }

        [Test]
        public async Task GetUsers_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetAllUsers>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<User>());

            var controller = new UsersController(_mockConfiguration.Object, _mockUserManager.Object, _mockRoleManager.Object, _mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.GetUsers();

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task AssignRole_WhenCalled_ReturnsBadRequest()
        {
            // Arrange
            _mockUserManager.Setup(m => m.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(_user);

            var controller = new UsersController(_mockConfiguration.Object, _mockUserManager.Object, _mockRoleManager.Object, _mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.AssignRole("carl.testito@example.com", "Doctor");

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }
    }
}
