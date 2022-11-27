using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Queries
{
	public class GetAllOffices : IRequest<List<Office>>
	{

	}
}
