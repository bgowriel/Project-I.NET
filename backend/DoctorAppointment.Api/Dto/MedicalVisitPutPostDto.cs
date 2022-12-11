namespace DoctorAppointment.Api.Dto
{
    public class MedicalVisitPutPostDto
    {
        public DateTime Date { get; set; }

        public string? Description { get; set; }

        public string? DoctorId { get; set; }

        public string? PatientId { get; set; }
    }
}
