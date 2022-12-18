using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.QueryHandlers
{
	public class GetAppointmentByDateHandler : IRequestHandler<GetAppointmentsByDate, List<Appointment>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAppointmentByDateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Appointment>> Handle(GetAppointmentsByDate request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return new List<Appointment>();
            }
            var appointments = await _unitOfWork.AppointmentRepository.GetByDate(request.Date);
            appointments ??= new List<Appointment>();
            return appointments;
        }
    }
}
