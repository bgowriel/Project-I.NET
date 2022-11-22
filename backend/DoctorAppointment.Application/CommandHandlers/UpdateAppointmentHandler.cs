using DoctorAppointment.Application.Commands;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.CommandHandlers
{
    public class UpdateAppointmentHandler : IRequestHandler<UpdateAppointment, Appointment>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAppointmentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointment> Handle(UpdateAppointment request, CancellationToken cancellationToken)
        {
            var toUpdate = new Appointment
            {
                Id = request.Id,
                Date = request.Date,
                Description = request.Description,
                Status = request.Status,
                DoctorId = request.DoctorId,
                PatientId = request.PatientId,
            };
            
            _unitOfWork.AppointmentRepository.Update(toUpdate);
            await _unitOfWork.Save();
            
            return toUpdate;
        }
    }
}
