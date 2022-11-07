namespace DoctorAppointment.Domain.Models.Request
{
    public class Payment
    {
        public Bill Bill { get; private set; }
        public DateTime Date { get; private set; }
        public int Amount { get; private set; }
        public User Patient { get; private set; }
        
    }
}
