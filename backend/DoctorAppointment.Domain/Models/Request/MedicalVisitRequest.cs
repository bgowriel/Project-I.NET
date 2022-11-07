namespace DoctorAppointment.Domain.Models.Request
{
    public class MedicalVisitRequest
    {
        public string Title { get; private set; }

        public string Type { get; private set; }

        public string Description { get; private set; }

        public AppointmentRequest Appointment { get; private set; }

        public BillRequest? Bill { get; private set; }
    }
}
