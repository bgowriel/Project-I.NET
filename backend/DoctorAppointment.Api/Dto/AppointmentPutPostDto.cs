namespace DoctorAppointment.Api.Dto
{
    public class AppointmentPutPostDto
    {
        public DateTime Date { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }

        public string? DoctorId { get; set; }

        public string? PatientId { get; set; }

        public Guid? OfficeId { get; set; }
    }
}
