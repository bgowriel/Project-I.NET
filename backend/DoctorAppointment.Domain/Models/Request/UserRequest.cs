namespace DoctorAppointment.Domain.Models.Request
{
    public class UserRequest
    {
        public UserRequest()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public string? Name { get; set; }

        public string? Email { get; set; }
        
        public string? Phone { get; set; }
        
        public string? Address { get; set; }

        public string? Role { get; set; } //client or doctor

        public List<AppointmentRequest>? Appointments { get; set; }
        
        public List<MedicalVisitRequest>? MedicalVisits { get; set; }        
    }
}
