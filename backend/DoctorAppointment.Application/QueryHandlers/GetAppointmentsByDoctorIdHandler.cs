using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.QueryHandlers
{
    public class GetAppointmentsByDoctorIdHandler : IRequestHandler<GetAppointmentsByDoctorId, List<Appointment>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAppointmentsByDoctorIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Appointment>> Handle(GetAppointmentsByDoctorId request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new NullReferenceException("Request is null");
            }
            
            var appointments = await _unitOfWork.AppointmentRepository.GetByDoctorId(request.DoctorId);
            if (appointments == null)
            {
                throw new NullReferenceException("No appointments found");
            }
            return appointments;
        }
    }
}
