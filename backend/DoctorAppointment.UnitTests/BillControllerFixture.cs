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
	public class BillControllerFixture
	{
		private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
		private Mock<IMapper> _mockMapper;
		private BillPutPostDto _billPutPostDto;
		private BillGetDto _billGetDto;
		private Bill _bill;

		[SetUp]
		public void Setup()
		{
			_billPutPostDto = new BillPutPostDto
			{
				Date = DateTime.Now,
				Amount = 100,
				DoctorId = "Test",
				PatientId = "Test"
			};

			_billGetDto = new BillGetDto
			{
				Id = new Guid(),
				Date = DateTime.Now,
				Amount = 100,
				DoctorId = "Test",
				PatientId = "Test"
			};

			_bill = new Bill
			{
				Id = new Guid(),
				Date = DateTime.Now,
				Amount = 100,
				DoctorId = "Test",
				PatientId = "Test"
			};

			_mockMapper = MappingData();
		}

		private Mock<IMapper> MappingData()
		{
			var mockMapper = new Mock<IMapper>();
			mockMapper.Setup(m => m.Map<BillGetDto>(It.IsAny<Bill>())).Returns(_billGetDto);
			mockMapper.Setup(m => m.Map<Bill>(It.IsAny<BillPutPostDto>())).Returns(_bill);
			return mockMapper;
		}

		[Test]
		public async Task InsertBillAddsANewBill()
		{
			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<InsertBill>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(_bill);

			// Act
			var billController = new BillController(_mockMediator.Object, _mockMapper.Object);

			var result = await billController.AddBill(_billPutPostDto);

			// Assert
			Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());

		}

		[Test]
		public async Task GetAllBillsReturnsAllBillsWithStatusOk()
		{
			// Arrange
			var bills = new List<Bill> { _bill };
			_mockMediator.Setup(m => m.Send(It.IsAny<GetAllBills>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(bills);

			// Act
			var billController = new BillController(_mockMediator.Object, _mockMapper.Object);
			var result = await billController.GetBills();

			// Assert
			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}


		[Test]
		public async Task GetBillByIdReturnsBillWithGivenId()
		{
			// Arrange
			_mockMediator.Setup(m => m.Send(It.IsAny<GetBillById>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(_bill);

			// Act
			var billController = new BillController(_mockMediator.Object, _mockMapper.Object);

			var result = await billController.GetBillById(_bill.Id);

			// Assert
			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

        [Test]
        public async Task GetBillByIdReturnsNotFoundIfBillIsNotFound()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(It.IsAny<GetBillById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Bill());

            // Act
            var billController = new BillController(_mockMediator.Object, _mockMapper.Object);

            var result = await billController.GetBillById(_bill.Id);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task GetBillsByPatientIdReturnsOk()
        {
            // Arrange
            var bills = new List<Bill> { _bill };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetBillsByPatientId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(bills);

            // Act
            var billController = new BillController(_mockMediator.Object, _mockMapper.Object);

			if (_bill.PatientId == null)
            {
                throw new ArgumentNullException(nameof(_bill.PatientId));
            }

            var result = await billController.GetBillsByPatientId(_bill.PatientId);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetBillsByDoctorIdReturnsOk()
        {
            // Arrange
            var bills = new List<Bill> { _bill };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetBillsByDoctorId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(bills);

            // Act
            var billController = new BillController(_mockMediator.Object, _mockMapper.Object);

			if (_bill.DoctorId == null)
			{
                throw new ArgumentNullException(nameof(_bill.DoctorId));
            }
            var result = await billController.GetBillsByDoctorId(_bill.DoctorId);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
    }
}
