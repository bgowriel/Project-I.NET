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
            var appointments = await _unitOfWork.AppointmentRepository.GetByDate(request.Date);
            if (appointments == null)
            {
                throw new Exception("appointments is null");
            }
            return appointments;
        }
    }
}
