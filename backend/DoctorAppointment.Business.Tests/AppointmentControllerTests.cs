using DoctorAppointment.Domain.Models.Request;
using DoctorAppointment.Domain.Models.Response;
using FluentAssertions;
using System.Net.Http.Json;

namespace DoctorAppointment.Business.Tests
{
    public class AppointmentControllerTests : BaseIntegrationTests
    {
        const string ApiURL = "api/appointments";
        private static AppointmentRequest CreateSUT()
        {
            return new AppointmentRequest()
            {
                Name = "MyAppointment",
                Date = DateTime.Now,
                DoctorId = Guid.NewGuid(),
                PatientId = Guid.NewGuid(),
                ServiceProvidedId = Guid.NewGuid()
            };

        }

        [Fact]
        public async void When_CreatedAppointment_Then_ShouldReturnAppointment()
        {
            AppointmentRequest appointmentRequest = CreateSUT();
            //Act

            var createAppointmentResponse = await HttpClient.PostAsJsonAsync(ApiURL, appointmentRequest);
            var getAppointmentResult = await HttpClient.GetAsync(ApiURL);

            //Assert

            createAppointmentResponse.EnsureSuccessStatusCode();
            createAppointmentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getAppointmentResult.EnsureSuccessStatusCode();
            var appointments = await getAppointmentResult.Content.ReadFromJsonAsync<List<AppointmentResponse>>();
            appointments.Count.Should().Be(1);
            appointments.Should().HaveCount(1);
            appointments.Should().NotBeNull();
        }

    }
}
