namespace DoctorAppointment.Api.Dto
{
    public class BillPutPostDto
    {
        public DateTime Date { get; set; }

        public double Amount { get; set; }

        public string PatientId { get; set; }

        public string DoctorId { get; set; }

        public Guid MedicalVisitId { get; set; }
    }
}
