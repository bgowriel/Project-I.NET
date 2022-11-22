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
    public class GetAppointmentByIdHandler : IRequestHandler<GetAppointmentById, Appointment>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAppointmentByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointment> Handle(GetAppointmentById request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AppointmentRepository.GetById(request.Id);
        }
    }
}
