namespace DoctorAppointment.Domain.Models.Request
{
    public class Bill
    {
        public string? Code { get; private set; }
        public Doctor? Doctor { get; private set; }
        public float? Total { get; private set; }
        public DateTime? StatementDate { get; private set; }
        public DateTime? DueDate { get; private set; }
        public Guid UserId { get; private set; }
        List<ServiceProvided> ServicesProvided { get; set; }
    }
}
