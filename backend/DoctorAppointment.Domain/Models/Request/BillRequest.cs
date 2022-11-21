using DoctorAppointment.Domain.Models.Response;

namespace DoctorAppointment.Domain.Models.Request
{
    public class BillRequest
    {
        public string? Code { get; private set; }

        public float? Total { get; private set; }

        public DateTime? StatementDate { get; private set; }

        public DateTime? DueDate { get; private set; }

        public Guid UserId { get; private set; }

        public Guid? PaymentId { get; set; }
    }
}
