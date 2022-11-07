namespace DoctorAppointment.Domain.Models.Request
{
    public class PaymentRequest
    {
        public BillRequest Bill { get; private set; }
        
        public DateTime Date { get; private set; }

        public float? Total { get; private set; }        
    }
}
