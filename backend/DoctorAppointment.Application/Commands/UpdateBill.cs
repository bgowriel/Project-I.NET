using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Commands
{
    public class UpdateBill : IRequest<Bill>
    {
        public Guid Id { get; set; }
        
        public DateTime Date { get; set; }
        
        public double Amount { get; set; }
        
        public string? PatientId { get; set; }
        
        public string? DoctorId { get; set; }
        
        public Guid MedicalVisitId { get; set; }
    }
}
