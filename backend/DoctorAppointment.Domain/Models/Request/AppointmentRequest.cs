namespace DoctorAppointment.Domain.Models.Request
{
    public class AppointmentRequest
    {
        public string Name { get;  set; }

        public DateTime Date { get;  set; }

        public Guid DoctorId { get;  set; }

        public Guid PatientId { get;  set; }

        public Guid ServiceProvidedId { get; set; }

    }
}
