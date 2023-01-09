using System.Diagnostics.CodeAnalysis;

namespace DoctorAppointment.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public class AvailableDate
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public bool Free { get; set; }
    }
}
