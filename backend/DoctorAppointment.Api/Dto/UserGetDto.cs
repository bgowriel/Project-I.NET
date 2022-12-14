namespace DoctorAppointment.Api.Dto
{
	public class UserGetDto
	{
		public string? Id { get; set; }
		
		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public string? Role { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public Guid? OfficeId { get; set; }

	}
}
