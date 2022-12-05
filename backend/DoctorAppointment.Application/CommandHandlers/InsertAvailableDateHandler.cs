using DoctorAppointment.Application.Commands;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.CommandHandlers
{
    public class InsertAvailableDateHandler : IRequestHandler<InsertAvailableDate, AvailableDate>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsertAvailableDateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AvailableDate> Handle(InsertAvailableDate request, CancellationToken cancellationToken)
        {
            var availableDate = new AvailableDate
            {
                Id = Guid.NewGuid(),
                Date = request.Date,
                Free = request.Free
            };

            await _unitOfWork.AvalaibleDateRepository.Insert(availableDate);

            await _unitOfWork.Save();

            return availableDate;
        }
    }
}
