using AutoMapper;
using DoctorAppointment.Api.Controllers;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Domain.Models;
using DoctorAppointment.Application.Commands;
using MediatR;
using Moq;
using Microsoft.AspNetCore.Mvc;
using DoctorAppointment.Application.Queries;

namespace DoctorAppointment.UnitTests
{
    [TestFixture]
    public class AppointmentControllerFixture
    {
        private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
        private Mock<IMapper> _mockMapper;
        private AppointmentPutPostDto _appointmentPutPostDto;
        private AppointmentGetDto _appointmentGetDto;
        private Appointment _appointment;

        [SetUp]
        public void Setup()
        {
            _appointmentPutPostDto = new AppointmentPutPostDto
            {
                Date = DateTime.Now,
                Description = "Test",
                Status = "Pending",
                DoctorId = "Test",
                PatientId = "Test"
            };

            _appointmentGetDto = new AppointmentGetDto
            {
                Id = new Guid(),
                Date = DateTime.Now,
                Description = "Test",
                Status = "Pending",
                DoctorId = "Test",
                PatientId = "Test"
            };

            _appointment = new Appointment
            {
                Id = new Guid(),
                Date = DateTime.Now,
                Description = "Test",
                Status = "Pending",
                DoctorId = "Test",
                PatientId = "Test"
            };

            _mockMapper = MappingData();
        }

        private Mock<IMapper> MappingData()
        {
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<AppointmentGetDto>(It.IsAny<Appointment>())).Returns(_appointmentGetDto);
            mockMapper.Setup(m => m.Map<Appointment>(It.IsAny<AppointmentPutPostDto>())).Returns(_appointment);
            return mockMapper;
        }

        [Test]
        public async Task InsertAppointmentAddsANewAppointment()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<InsertAppointment>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_appointment);

            var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.AddAppointment(_appointmentPutPostDto);

			// Assert
			Assert.That(result, Is.InstanceOf(typeof(CreatedAtActionResult)));
		}
	

        [Test]
        public async Task UpdateAppointmentUpdatesAnExistingAppointment()
		{
			// Arrange
			AppointmentPutPostDto appointmentToUpdate = new AppointmentPutPostDto
            {
                Date = DateTime.Now,
                Description = "Test",
                Status = "Approved",
                DoctorId = "1",
                PatientId = "1"
            };
            _appointmentGetDto.Status = "Approved";
            _appointmentGetDto.DoctorId = "1";
            _appointmentGetDto.PatientId = "1";

            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateAppointment>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_appointment);

            var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object);


            // Act
            var result = await controller.UpdateAppointment(_appointment.Id, appointmentToUpdate);

            // Assert
            Console.WriteLine(result.ToString());
			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

        [Test]
        public async Task UpdateAppointmentReturnsNotFound()
        {
            // Arrange
            AppointmentPutPostDto appointmentToUpdate = new AppointmentPutPostDto
            {
                Date = DateTime.Now,
                Description = "Test",
                Status = "Approved",
                DoctorId = "1",
                PatientId = "1"
            };
            _appointmentGetDto.Status = "Approved";
            _appointmentGetDto.DoctorId = "1";
            _appointmentGetDto.PatientId = "1";

            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateAppointment>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Appointment)null);

            var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.UpdateAppointment(_appointment.Id, appointmentToUpdate);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]  
		public async Task GetAllAppointmentsReturnAllExistingAppointments()
        {
			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<GetAllAppointments>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new List<Appointment> { _appointment });

			var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object);

			// Act
			var result = await controller.GetAppointments();

			// Assert
			Assert.That(result, Is.InstanceOf<OkObjectResult>());

		}

		[Test]
		public async Task GetAppointmentByIdReturnsAppointmentByGivenId()
        {
			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<GetAppointmentById>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(_appointment);

			var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object);

			// Act
			var result = await controller.GetAppointmentById(_appointment.Id);

			// Assert
			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

        [Test]
        public async Task GetAppointmentByIdReturnsNotFound()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetAppointmentById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Appointment)null);

            var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.GetAppointmentById(_appointment.Id);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
		public async Task GetAppointmentsByDateReturnsAllAppointmentsByGivenDate()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetAppointmentsByDate>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new List<Appointment> { _appointment });
			var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object);

			// Act
			var result = await controller.GetAppointmentsByDate(_appointment.Date);

			// Assert
			Assert.That(result, Is.InstanceOf<OkObjectResult>());

		}

        [Test]
		public async Task GetAppointmentsByPatientIdReturnsAppointmentsByGivenPatientId()
        {
			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<GetAppointmentsByPatientId>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new List<Appointment> { _appointment });

			var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object);
            
			if (_appointment == null)
            {
				throw new Exception("Appointment not found");
			}

            if (_appointment.PatientId == null)
            {
				throw new Exception("PatientId not found");
			}
			
            // Act
            var result = await controller.GetAppointmentsByPatientId(_appointment.PatientId);

			if (result == null)
			{
				throw new Exception("Appointment not found");
			}

			// Assert
			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

		[Test]
		public async Task GetAppointmentsByDoctortIdReturnsAppointmentsByGivenDoctorId()
		{
			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<GetAppointmentsByPatientId>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new List<Appointment> { _appointment });

			var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object);

			if (_appointment == null)
            {
				throw new Exception("Appointment not found");
			}

			if (_appointment.DoctorId == null)
			{
				throw new Exception("DoctorId not found");
			}

			// Act
			var result = await controller.GetAppointmentsByPatientId(_appointment.DoctorId);

			// Assert
			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

        [Test]
		public async Task DeleteAppointmentDeletesAppointmentByGivenId()
		{
			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<DeleteAppointment>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(_appointment);

			var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object);

			// Act
			var result = await controller.DeleteAppointment(_appointment.Id);

			// Assert
			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

        [Test]
        public async Task DeleteAppointmentReturnsNotFound()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteAppointment>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Appointment)null);

            var controller = new AppointmentController(_mockMediator.Object, _mockMapper.Object);

            // Act
            var result = await controller.DeleteAppointment(_appointment.Id);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}