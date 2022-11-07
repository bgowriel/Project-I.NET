using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Models.Response
{
    public class AppointmentResponse
    {
        public int Id { get; set; }

        public string Name { get; private set; }

        public DateTime Date { get; private set; }

        public DoctorRequest Doctor { get; private set; }

        public UserRequest Patient { get; private set; }

        public ServiceProvidedRequest ServiceProvided { get; set; }

    }
}
