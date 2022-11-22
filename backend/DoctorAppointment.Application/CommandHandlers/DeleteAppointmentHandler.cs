using DoctorAppointment.Application.Commands;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.CommandHandlers
{
    public class DeleteAppointmentHandler : IRequestHandler<DeleteAppointment, Appointment>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAppointmentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointment> Handle(DeleteAppointment request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetById(request.Id);

            if (appointment == null)
            {
                return null;
            }

            _unitOfWork.AppointmentRepository.Delete(appointment);
            await _unitOfWork.Save();

            return appointment;
        }
    }
}
