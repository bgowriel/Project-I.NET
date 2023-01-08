using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Commands
{
    public class InsertAppointment : IRequest<Appointment>
    {
        public Guid Id { get; set; }
        
        public DateTime Date { get; set; }

        public int Hour { get; set; }

        public string? Description { get; set; }
        
        public string? Status { get; set; }
        
        public string? DoctorId { get; set; }
        
        public string? PatientId { get; set; }
		
        public Guid OfficeId { get; set; }

		public Guid? BillId { get; set; }
	}
}