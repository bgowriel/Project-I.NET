﻿using DoctorAppointment.Application.Interfaces;
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
    public class GetAppointmentByDateHandler : IRequestHandler<GetAppointmentsByDate, List<Appointment>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAppointmentByDateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Appointment>> Handle(GetAppointmentsByDate request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AppointmentRepository.GetByDate(request.Date);
        }
    }
}
