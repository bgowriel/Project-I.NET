namespace DoctorAppointment.Domain.Models.Request
{
    public class AppointmentRequest
    {
        public string Name { get; private set; }

        public DateTime Date { get; private set; }

        public Guid DoctorId { get; private set; }

        public Guid PatientId { get; private set; }

        public Guid ServiceProvidedId { get; set; }

    }
}
