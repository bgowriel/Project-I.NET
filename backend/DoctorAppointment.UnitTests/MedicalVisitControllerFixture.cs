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
	public class MedicalVisitControllerFixture
	{
		private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
		private Mock<IMapper> _mockMapper;
		private MedicalVisitPutPostDto _medicalVisitPutPostDto;
		private MedicalVisitGetDto _medicalVisitGetDto;
		private MedicalVisit _medicalVisit;

		[SetUp]
		public void Setup()
		{
			_medicalVisitPutPostDto = new MedicalVisitPutPostDto
			{
				Date = DateTime.Now,
				Description = "Test",
				DoctorId = "Test",
				PatientId = "Test"
			};

			_medicalVisitGetDto = new MedicalVisitGetDto
			{
				Id = new Guid(),
				Date = DateTime.Now,
				Description = "Test",
				DoctorId = "Test",
				PatientId = "Test"
			};

			_medicalVisit = new MedicalVisit
			{
				Id = new Guid(),
				Date = DateTime.Now,
				Description = "Test",
				DoctorId = "Test",
				PatientId = "Test"
			};

			_mockMapper = MappingData();
		}

		private Mock<IMapper> MappingData()
		{
			var mockMapper = new Mock<IMapper>();
			mockMapper.Setup(m => m.Map<MedicalVisitGetDto>(It.IsAny<MedicalVisit>())).Returns(_medicalVisitGetDto);
			mockMapper.Setup(m => m.Map<MedicalVisit>(It.IsAny<MedicalVisitPutPostDto>())).Returns(_medicalVisit);
			return mockMapper;
		}

		[Test]
		public async Task InsertMedicalVisitAddsANewMedicalVisit()
		{
			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<InsertMedicalVisit>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(_medicalVisit);

			var controller = new MedicalVisitController(_mockMediator.Object, _mockMapper.Object);

			// Act
			var result = await controller.AddMedicalVisit(_medicalVisitPutPostDto);

			// Assert
			Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
		}

		[Test]
		public async Task GetMedicalVisitsReturnsAllMedicalVisits()
		{
			// Arrange
			var medicalVisits = new List<MedicalVisit> { _medicalVisit };
			_mockMediator.Setup(m => m.Send(It.IsAny<GetAllMedicalVisits>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(medicalVisits);

			var controller = new MedicalVisitController(_mockMediator.Object, _mockMapper.Object);

			// Act
			var result = await controller.GetMedicalVisits();

			// Assert
			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

		[Test]
		public async Task GetMedicalVisitByIdReturnMedicalVisitByGivenId()
		{
			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<GetMedicalVisitById>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(_medicalVisit);

			var controller = new MedicalVisitController(_mockMediator.Object, _mockMapper.Object);

			// Act
			var result = await controller.GetMedicalVisitById(_medicalVisit.Id);

			// Assert
			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

		[Test]
        public async Task GetMedicalVisitByIdReturnNotFound()
        {
			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<GetMedicalVisitById>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new MedicalVisit());

            var controller = new MedicalVisitController(_mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.GetMedicalVisitById(_medicalVisit.Id);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
	
}
