namespace DoctorAppointment.Domain.Models.Response
{
    public class DoctorResponse
    {
        public DoctorResponse(string? name, string? email, string? phone, string? address,
            string? specialization, string? description, string? image, string? role,
            string? token, string? status)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            Specialization = specialization;
            Description = description;
            Image = image;
            Role = role;
            Token = token;
            Status = status;
            Appointments = new List<AppointmentResponse>();
        }

        public Guid Id { get; private set; }

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
        public List<AppointmentResponse>? Appointments { get; set; }
    }
}
