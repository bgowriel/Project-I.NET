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
