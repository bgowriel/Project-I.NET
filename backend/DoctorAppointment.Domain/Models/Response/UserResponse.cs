namespace DoctorAppointment.Domain.Models.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        
        public string? Name { get; set; }
        
        public string? Email { get; set; }
        
        public string? Phone { get; set; }
        
        public string? Address { get; set; }
        
        public string? Role { get; set; } //client or doctor
        
        public List<AppointmentResponse>? Appointments { get; set; }

        public List<BillResponse> Bills { get; set; }
    }
}
