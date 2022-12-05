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
	public class AvailableDateControllerFixture
	{
		private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
		private Mock<IMapper> _mockMapper;
		private AvailableDatePutPostDto _availableDatePutPostDto;
		private AvailableDateGetDto _availableDateGetDto;
		private AvailableDate _availableDate;

		[SetUp]
		public void Setup()
		{
			_availableDatePutPostDto = new AvailableDatePutPostDto
			{
				Date = DateTime.Now,
				Free = true
			};

			_availableDateGetDto = new AvailableDateGetDto
			{
				Id = new Guid(),
				Date = DateTime.Now,
				Free = true
			};

			_availableDate = new AvailableDate
			{
				Id = new Guid(),
				Date = DateTime.Now,
				Free = true
			};

			_mockMapper = MappingData();
		}

		private Mock<IMapper> MappingData()
		{
			var mockMapper = new Mock<IMapper>();
			mockMapper.Setup(m => m.Map<AvailableDateGetDto>(It.IsAny<AvailableDate>())).Returns(_availableDateGetDto);
			mockMapper.Setup(m => m.Map<AvailableDate>(It.IsAny<AvailableDatePutPostDto>())).Returns(_availableDate);
			return mockMapper;
		}

		[Test]
		public async Task InsertAvailableDateAddsANewAvailableDate()
		{
			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<InsertAvailableDate>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(_availableDate);

			var controller = new AvailableDateController(_mockMediator.Object, _mockMapper.Object);

			// Act
			var result = await controller.AddAvailableDate(_availableDatePutPostDto);

			// Assert
			Assert.IsInstanceOf<CreatedAtActionResult>(result);
			
		}

		[Test]
		public async Task GetAvailableDatesReturnsAllAvailableDatesWithOk()
		{
			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<GetAllAvailableDates>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new List<AvailableDate> { _availableDate });

			var controller = new AvailableDateController(_mockMediator.Object, _mockMapper.Object);

			// Act
			var result = await controller.GetAvailableDates();

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(result);
		}

		[Test]
		public async Task GetAvailableDateByIdReturnsAvailableDateWithGivenId()
		{
			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<GetAvailableDateById>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(_availableDate);

			var controller = new AvailableDateController(_mockMediator.Object, _mockMapper.Object);

			// Act
			var result = await controller.GetAvailableDateById(_availableDate.Id);

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(result);
		}
	}
	
}
