namespace DoctorAppointment.Domain.Models
{
    public class AvailableDate
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public bool Free { get; set; }
    }
}
