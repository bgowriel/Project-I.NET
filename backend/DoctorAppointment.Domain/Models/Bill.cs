using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.Domain.Models
{
    public class Bill
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public double Amount { get; set; }

        public string? PatientId { get; set; }

        public User? Patient { get; set; }

        public string? DoctorId { get; set; }

        public User? Doctor { get; set; }

        public Guid MedicalVisitId { get; set; }

        public MedicalVisit? MedicalVisit { get; set; }
    }
}
