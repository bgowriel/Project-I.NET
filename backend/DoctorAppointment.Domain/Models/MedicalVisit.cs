using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DoctorAppointment.Domain.Models
{
    [ExcludeFromCodeCoverage]    
    public class MedicalVisit
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(500)]
        public string? Description { get; set; }

        public string? DoctorId { get; set; }

        public User? Doctor { get; set; }

        public string? PatientId { get; set; }

        public User? Patient { get; set; }
    }
}
