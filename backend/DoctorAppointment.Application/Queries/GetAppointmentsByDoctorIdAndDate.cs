using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
	public class GetAppointmentsByDoctorIdAndDate : IRequest<List<Appointment>>
	{
		public string? DoctorId { get; set; }
		public DateTime Date { get; set; }
	}
}