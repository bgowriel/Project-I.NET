namespace DoctorAppointment.Domain.Models.Request
{
    public class MedicalVisit
    {
        public string Name { get; private set; }

        public string Type { get; private set; }

        public string Description { get; private set; }

        public Appointment Appointment { get; private set; }
    }
}
