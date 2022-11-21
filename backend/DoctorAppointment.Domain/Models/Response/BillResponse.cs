using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Models.Response
{
    public class BillResponse
    {
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
