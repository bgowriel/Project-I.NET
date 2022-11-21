namespace DoctorAppointment.Domain.Models.Request
{
    public class BillRequest
    {
        public BillRequest()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public string? Code { get; private set; }

        public DoctorRequest? Doctor { get; private set; }

        public float? Total { get; private set; }

        public DateTime? StatementDate { get; private set; }

        public DateTime? DueDate { get; private set; }

        public Guid UserId { get; private set; }

        List<ServiceProvidedRequest> ServicesProvided { get; set; }

        public Guid? PaymentId { get; set; }
    }
}
