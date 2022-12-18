using DoctorAppointment.Application.Exceptions;
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
            if (request == null)
            {
                throw new NotFoundException("Appointment Id is null");
            }
            
            var appointment = await _unitOfWork.AppointmentRepository.GetById(request.Id);
            if (appointment == null)
            {
                throw new NotFoundException("No appointment found with this Id");
            }
            return appointment;
        }
    }
}
