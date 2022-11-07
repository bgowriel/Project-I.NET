using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Models.Response
{
    public class PaymentResponse
    {
        public int Id { get; set; }

        public BillResponse Bill { get; private set; }
        
        public DateTime Date { get; private set; }

        public float? Total { get; private set; }
    }
}
