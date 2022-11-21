namespace DoctorAppointment.Domain.Models.Request
{
    public class AdminRequest
    {
        public AdminRequest()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public string Name { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
