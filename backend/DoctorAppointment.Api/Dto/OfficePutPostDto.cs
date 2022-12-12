using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.Api.Dto
{
    public class OfficePutPostDto
	{
		[Required]
		[MinLength(3)]
		[MaxLength(100)]
		public string? Name { get; set; }

		public string? Description { get; set; }

		[Required]
		[MinLength(3)]
		[MaxLength(100)]
		public string? Address { get; set; }

		[Required]
		[MinLength(3)]
		[MaxLength(100)]
		public string? City { get; set; }

		[EmailAddress]
		[MinLength(6)]
		[Required(ErrorMessage = "Email is required")]
		public string? Email { get; set; }

		[PhoneAttribute]
		[Required(ErrorMessage = "Phone number is required")]
		public string? Phone { get; set; }


	}
}
