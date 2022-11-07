namespace DoctorAppointment.Domain.Models.Request
{
    public class AppointmentRequest
    {

        public string Name { get; private set; }

        public DateTime Date { get; private set; }

        public DoctorRequest Doctor { get; private set; }

        public UserRequest Patient { get; private set; }

        public ServiceProvidedRequest ServiceProvided {get; set;}

    }
}
