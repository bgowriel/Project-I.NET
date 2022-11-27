using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.QueryHandlers
{
	public class GetAllAppointmentsHandler : IRequestHandler<GetAllAppointments, List<Appointment>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAppointmentsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Appointment>> Handle(GetAllAppointments request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AppointmentRepository.GetAll();
        }
    }
}
