using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.Domain.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string? Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        public string DoctorId { get; set; }

        public User Doctor { get; set; }

        public string PatientId { get; set; }

        public User Patient { get; set; } 

        public Guid? OfficeId { get; set; }
    }
}
