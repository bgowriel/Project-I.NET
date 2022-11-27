using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return await _unitOfWork.AppointmentRepository.GetByPatientId(request.PatientId);
        }
    }
}
