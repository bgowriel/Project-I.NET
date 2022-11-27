using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Commands
{
	public class DeleteOffice : IRequest<Office>
	{
		public Guid Id { get; set; }
	}
}
