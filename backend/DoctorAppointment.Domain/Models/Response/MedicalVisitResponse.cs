namespace DoctorAppointment.Domain.Models.Response
{
    public class MedicalVisitResponse
    {
        public Guid Id { get; private set; }

        public string Title { get; private set; }

        public string Type { get; private set; }

        public string Description { get; private set; }

        public AppointmentResponse Appointment { get; private set;}
        
        public BillResponse Bill { get; private set; }
    }
}
