using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
	public class GetOfficeById : IRequest<Office>
	{
		public Guid Id { get; set; }
	}
}
