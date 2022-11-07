namespace DoctorAppointment.Domain.Models.Response
{
    public class MedicalVisit
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public string Type { get; private set; }

        public string Description { get; private set; }

        public Appointment Appointment { get; private set;}
    }
}
