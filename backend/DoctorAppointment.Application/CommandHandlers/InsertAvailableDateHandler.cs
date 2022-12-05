using DoctorAppointment.Application.Commands;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.CommandHandlers
{
    public class InsertAvailableDateHandler : IRequestHandler<InsertAvailableDate, AvalaibleDate>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsertAvailableDateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AvalaibleDate> Handle(InsertAvailableDate request, CancellationToken cancellationToken)
        {
            var availableDate = new AvalaibleDate
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
