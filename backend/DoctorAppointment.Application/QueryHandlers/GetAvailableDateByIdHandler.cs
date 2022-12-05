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
    public class GetAvailableDateByIdHandler : IRequestHandler<GetAvailableDateById, AvalaibleDate>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAvailableDateByIdHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<AvalaibleDate> Handle(GetAvailableDateById request, CancellationToken cancellationToken)
        {
            return await unitOfWork.AvalaibleDateRepository.GetById(request.Id);
        }
    }
    
}
