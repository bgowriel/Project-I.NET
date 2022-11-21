namespace DoctorAppointment.Domain.Models.Request
{
    public class PaymentRequest
    {
        public PaymentRequest()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public BillRequest Bill { get; private set; }
        
        public DateTime Date { get; private set; }

        public float? Total { get; private set; }        
    }
}
