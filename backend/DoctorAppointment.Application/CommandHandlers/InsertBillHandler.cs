using DoctorAppointment.Application.Commands;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.CommandHandlers
{
    public class InsertBillHandler : IRequestHandler<InsertBill, Bill>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsertBillHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Bill> Handle(InsertBill request, CancellationToken cancellationToken)
        {
            var bill = new Bill
            {
                Id = Guid.NewGuid(),
				Date = request.Date,
				Description = request.Description,
                Amount = request.Amount,
                PatientId = request.PatientId,
                DoctorId = request.DoctorId
			};

            await _unitOfWork.BillRepository.Insert(bill);
            await _unitOfWork.Save();

            return bill;
        }
    }
}
