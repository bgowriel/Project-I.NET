namespace DoctorAppointment.Domain.Models.Response
{
    public class Appointment
    {
        public int Id { get; set; }
        
        public string Name { get; private set; }

        public DateTime Date { get; private set; }

        //public Doctor Doctor { get; private set; }

        public User Patient { get; private set; }

    }
}
