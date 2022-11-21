namespace DoctorAppointment.Domain.Models.Response
{
    public class PaymentResponse
    {
        public Guid Id { get; private set; }

        public BillResponse Bill { get; private set; }
        
        public DateTime Date { get; private set; }

        public float? Total { get; private set; }
    }
}
