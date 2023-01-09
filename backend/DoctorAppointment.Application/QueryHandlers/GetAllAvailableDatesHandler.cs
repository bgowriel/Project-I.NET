using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;

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
            availableDates ??= new List<AvailableDate>();
            return availableDates;
           
        }
    }

}
