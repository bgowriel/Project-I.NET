namespace DoctorAppointment.Api.Dto
{
    public class MedicalVisitGetDto
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string? Description { get; set; }

        public string DoctorId { get; set; }

        public string PatientId { get; set; }
    }
}
