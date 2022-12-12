using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.QueryHandlers
{
	public class GetAppointmentByIdHandler : IRequestHandler<GetAppointmentById, Appointment>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAppointmentByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointment> Handle(GetAppointmentById request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetById(request.Id);
            if (appointment == null)
            {
                throw new Exception("appointment is null");
            }
            return appointment;
        }
    }
}
