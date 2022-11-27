using DoctorAppointment.Application.Commands;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.CommandHandlers
{
    public class InsertAppointmentHandler : IRequestHandler<InsertAppointment, Appointment>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsertAppointmentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointment> Handle(InsertAppointment request, CancellationToken cancellationToken)
        {
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                Date = request.Date,
                Description = request.Description,
                Status = request.Status,
                DoctorId = request.DoctorId,
                PatientId = request.PatientId,
                OfficeId = request.OfficeId
            };

            await _unitOfWork.AppointmentRepository.Insert(appointment);
            await _unitOfWork.Save();

            return appointment;
        }
    }
}
