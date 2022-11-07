using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime Date { get; private set; }

        public int Amount { get; private set; }
        public User Patient { get; private set; }

    }
}
