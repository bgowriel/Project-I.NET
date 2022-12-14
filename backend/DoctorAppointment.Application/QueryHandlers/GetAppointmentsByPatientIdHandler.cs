using DoctorAppointment.Application.Exceptions;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.QueryHandlers
{
    internal class GetAppointmentsByPatientIdHandler : IRequestHandler<GetAppointmentsByPatientId, List<Appointment>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAppointmentsByPatientIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Appointment>> Handle(GetAppointmentsByPatientId request, CancellationToken cancellationToken)
        {
            if (request.PatientId == null)
            {
                throw new NotFoundException("PatientId is null");
            }
            var appointments = await _unitOfWork.AppointmentRepository.GetByPatientId(request.PatientId);
            if (appointments == null)
            {
                return new List<Appointment>();
            }
            return appointments;
        }
    }
}
