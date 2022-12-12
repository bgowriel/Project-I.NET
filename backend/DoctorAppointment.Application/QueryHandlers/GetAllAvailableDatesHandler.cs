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
    public class GetAllAvailableDatesHandler : IRequestHandler<GetAllAvailableDates, List<AvailableDate>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAvailableDatesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AvailableDate>> Handle(GetAllAvailableDates request, CancellationToken cancellationToken)
        {
            var availableDates = await _unitOfWork.AvalaibleDateRepository.GetAll();
            if (availableDates == null)
            {
                throw new Exception("availableDates is null");
            }
            return availableDates;
           
        }
    }

}
