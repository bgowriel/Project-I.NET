namespace DoctorAppointment.Domain.Models.Response
{
    public class DoctorResponse
    {
        public Guid Id { get; private set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? Specialization { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public string? Role { get; set; }

        private string? Token { get; set; } //Paraph 

        private string? BankAccount { get; set; }

        public string? Status { get; set; } //Working, Not Working, On Vacation etc.

        public List<UserResponse>? Patients { get; set; }
        
        public List<AppointmentResponse>? Appointments { get; set; }
    }
}
