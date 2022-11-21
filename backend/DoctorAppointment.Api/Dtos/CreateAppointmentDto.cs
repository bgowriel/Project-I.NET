namespace DoctorAppointment.Api.Dtos
{
    public class CreateAppointmentDto
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public Guid DoctorId { get; set; }

        public Guid PatientId { get;  set; }

        public Guid ServiceProvidedId { get; set; }
    }
}
