using DoctorAppointment.Application.Exceptions;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.QueryHandlers
{
	public class GetAppointmentsByDoctorIdAndDateHandler : IRequestHandler<GetAppointmentsByDoctorIdAndDate, List<Appointment>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAppointmentsByDoctorIdAndDateHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<List<Appointment>> Handle(GetAppointmentsByDoctorIdAndDate request, CancellationToken cancellationToken)
		{
			if (request.DoctorId == null)
			{
				throw new NotFoundException("DoctorId is null");
			}

			var appointments = await _unitOfWork.AppointmentRepository.GetByDoctorIdAndDate(request.DoctorId,request.Date);
			if (appointments == null)
			{
				return new List<Appointment>();
			}
			return appointments;
		}
	}
}