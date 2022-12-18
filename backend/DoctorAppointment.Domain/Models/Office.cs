namespace DoctorAppointment.Domain.Models
{
	public class Office
	{
		public Guid Id { get; set; }

		public string? Name { get; set; }

		public string? Description { get; set; }

		public string? Address { get; set; }

		public string? City { get; set; }

		public string? Email { get; set; }

		public string? Phone { get; set; }

		public List<User>? Doctors { get; set; }

		public string? Status { get; set; }
	}
}
