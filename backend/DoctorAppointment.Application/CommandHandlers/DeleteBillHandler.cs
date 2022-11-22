using DoctorAppointment.Application.Commands;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.CommandHandlers
{
    public class DeleteBillHandler : IRequestHandler<DeleteBill, Bill>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBillHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Bill> Handle(DeleteBill request, CancellationToken cancellationToken)
        {
            var bill = await _unitOfWork.BillRepository.GetById(request.Id);

            if (bill == null)
            {
                return null;
            }

            _unitOfWork.BillRepository.Delete(bill);
            await _unitOfWork.Save();

            return bill;
        }
    }
}
