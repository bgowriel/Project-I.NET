namespace DoctorAppointment.Domain.Models.Request
{
    public class Appointment
    {

        public string Name { get; private set; }

        public DateTime Date { get; private set; }

        public Doctor Doctor { get; private set; }

        public User Patient { get; private set; }

    }
}
