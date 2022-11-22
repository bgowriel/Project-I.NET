using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.Api.Dto
{
    public class RegisterModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string Role { get; set; }

        [EmailAddress]
        [MinLength(6)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [MinLength(8)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
