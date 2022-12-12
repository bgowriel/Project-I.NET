using AutoMapper;
using DoctorAppointment.Api.Controllers;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Application.Commands;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DoctorAppointment.UnitTests
{
	[TestFixture]
	public class OfficeControllerFixture
	{
		private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
		private Mock<IMapper> _mockMapper;
		private OfficePutPostDto _officePutPostDto;
		private OfficeGetDto _officeGetDto;
		private Office _office;

		[SetUp]
		public void Setup()
		{
			_officePutPostDto = new OfficePutPostDto
			{
				Name = "Carl",
				Description = "Testito",
				Address = "Test",
				City = "Iasi",
				Email = "test@yahoo.com",
				Phone = "01298319024721"
			};

			_officeGetDto = new OfficeGetDto
			{
				Id = new Guid(),
				Name = "Carl",
				Description = "Testito",
				Address = "Test",
				City = "Iasi",
				Email = "test@yahoo.com",
				Phone = "01298319024721"
			};

			_office = new Office
			{
				Id = new Guid(),
				Name = "Carl",
				Description = "Testito",
				Address = "Test",
				City = "Iasi",
				Email = "test@yahoo.com",
				Phone = "01298319024721"
			};

			_mockMapper = MappingData();
		}

		private Mock<IMapper> MappingData()
		{
			var mockMapper = new Mock<IMapper>();
			mockMapper.Setup(m => m.Map<OfficeGetDto>(It.IsAny<Office>())).Returns(_officeGetDto);
			mockMapper.Setup(m => m.Map<Office>(It.IsAny<OfficePutPostDto>())).Returns(_office);
			return mockMapper;
		}

		[Test]
		public async Task InsertOfficeAddsANewOffice()
		{

			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<InsertOffice>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(_office);

			var controller = new OfficeController(_mockMediator.Object, _mockMapper.Object);

			// Act
			var result = await controller.AddOffice(_officePutPostDto);

			// Assert
			Console.WriteLine(result.ToString());
			Assert.IsInstanceOf<CreatedAtActionResult>(result);

		}

		[Test]
		public async Task GetAllOfficesReturnsAllOffices()
		{
			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<GetAllOffices>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new List<Office> { _office });

			var controller = new OfficeController(_mockMediator.Object, _mockMapper.Object);

			// Act
			var result = await controller.GetOffices();

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(result);
		}

		[Test]
		public async Task GetOfficeByIdReturnOfficeById()
		{
			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<GetOfficeById>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(_office);

			var controller = new OfficeController(_mockMediator.Object, _mockMapper.Object);

			// Act
			var result = await controller.GetOfficeById(_office.Id);

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(result);
		}

		[Test]
		public async Task GetDoctorsByOfficeIdReturnsDoctorsByOfficeId()
		{
			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<GetAllDoctors>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new List<User> { new User() });

			var controller = new OfficeController(_mockMediator.Object, _mockMapper.Object);

			// Act
			var result = await controller.GetDoctors(_office.Id);

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(result);
		}

		[Test]
		public async Task UpdateOfficeShouldUpdateAnExistingOffice()
		{
			// Arrange

			OfficePutPostDto officePutPostDto = new OfficePutPostDto
			{
				Name = "Carl",
				Description = "Testito",
				Address = "Test",
				City = "Iasi",
				Email = "carl@yahoo.com",
				Phone = "23123123123",
			};

			_officeGetDto.Email = "carl@yahoo.com";


			_mockMediator.Setup(m => m.Send(It.IsAny<UpdateOffice>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(_office);
			
			var controller = new OfficeController(_mockMediator.Object, _mockMapper.Object);
			// Act
			var result = await controller.UpdateOffice(_office.Id, officePutPostDto);

			// Assert
			Console.WriteLine(result.ToString());
			Assert.IsInstanceOf<OkObjectResult>(result);
			Assert.AreEqual(((OfficeGetDto)((OkObjectResult)result).Value).Email, "carl@yahoo.com");
		}

		[Test]
		public async Task DeleteOfficeShouldDeleteAnExistingOffice()
		{
			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<DeleteOffice>(), It.IsAny<CancellationToken>()));

			var controller = new OfficeController(_mockMediator.Object, _mockMapper.Object);

			// Act
			var result = await controller.DeleteOffice(_office.Id);

			// Assert
			Assert.IsInstanceOf<NotFoundResult>(result);
		}
	}
}
