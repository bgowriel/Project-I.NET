using DoctorAppointment.Domain.Models;
using FluentAssertions;
using System.Net.Http.Json;

namespace DoctorAppointment.Business.Tests
{
	public class AppointmentControllerTests : BaseIntegrationTests
    {
        const string ApiURL = "api/appointments";
        private static Appointment CreateSUT()
        {
            return new Appointment()
            {
                Date = DateTime.Now,
                Description = "Test",
                Status = "Pending",
                DoctorId = "1",
                PatientId = "2"
            };

        }

        [Fact]
        public async void When_CreatedAppointment_Then_ShouldReturnAppointment()
        {
            Appointment appointmentRequest = CreateSUT();
            //Act

            var createAppointmentResponse = await HttpClient.PostAsJsonAsync(ApiURL, appointmentRequest);
            var getAppointmentResult = await HttpClient.GetAsync(ApiURL);

            //Assert

            createAppointmentResponse.EnsureSuccessStatusCode();
            createAppointmentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getAppointmentResult.EnsureSuccessStatusCode();
            var appointments = await getAppointmentResult.Content.ReadFromJsonAsync<List<Appointment>>();
            appointments.Count.Should().Be(1);
            appointments.Should().HaveCount(1);
            appointments.Should().NotBeNull();
        }

    }
}
