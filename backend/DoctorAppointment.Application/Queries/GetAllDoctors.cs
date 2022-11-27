using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
	public class GetAllDoctors : IRequest<List<User>>
	{
		public Guid OfficeId { get; set; }

	}
}
