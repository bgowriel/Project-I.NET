using AutoMapper;
using DoctorAppointment.Api.Controllers;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Domain.Models;
using DoctorAppointment.Application.Commands;
using MediatR;
using Moq;
using Microsoft.AspNetCore.Mvc;

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
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
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
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(((AppointmentGetDto)((OkObjectResult)result).Value).Status, "Approved");
            Assert.AreEqual(((AppointmentGetDto)((OkObjectResult)result).Value).DoctorId, "1");
            Assert.AreEqual(((AppointmentGetDto)((OkObjectResult)result).Value).PatientId, "1");
        }
    }
}