using DoctorAppointment.Domain.Models.Response;

namespace DoctorAppointment.Domain.Models.Request
{
    public class DoctorRequest
    {
        
        public string? Name { get; set; }
        
        public string? Email { get; set; }
        
        public string? Phone { get; set; }
        
        public string? Address { get; set; }
        
        public string? Specialization { get; set; }
        
        public string? Description { get; set; }
        
        public string? Image { get; set; }
        
        public string? Role { get; set; }
        
        public string? Token { get; set; } //Paraph 
   
        public string? Status { get; set; } //Working, Not Working, On Vacation etc.
        
        public List<Guid>? Appointments { get; set; }
    }
}
