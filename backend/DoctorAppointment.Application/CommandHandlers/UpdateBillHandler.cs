using DoctorAppointment.Application.Commands;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.CommandHandlers
{
    public class UpdateBillHandler : IRequestHandler<UpdateBill, Bill>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBillHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Bill> Handle(UpdateBill request, CancellationToken cancellationToken)
        {
            var toUpdate = new Bill
            {
				Id = request.Id,   
				Date = request.Date,
				Description = request.Description,
				Amount = request.Amount,
				PatientId = request.PatientId,
				DoctorId = request.DoctorId
			};

            _unitOfWork.BillRepository.Update(toUpdate);
            await _unitOfWork.Save();

            return toUpdate;
        }
    }
}
