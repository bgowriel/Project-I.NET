using DoctorAppointment.Application.Exceptions;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.QueryHandlers
{
    public class GetAvailableDateByIdHandler : IRequestHandler<GetAvailableDateById, AvailableDate>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAvailableDateByIdHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<AvailableDate> Handle(GetAvailableDateById request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new NotFoundException("AvailableDate.Id is null");
            }
            var availableDate = await unitOfWork.AvalaibleDateRepository.GetById(request.Id);
            if (availableDate == null)
            {
                throw new NotFoundException("No available date with given Id was found");
            }
            return availableDate;
        }
    }
    
}
