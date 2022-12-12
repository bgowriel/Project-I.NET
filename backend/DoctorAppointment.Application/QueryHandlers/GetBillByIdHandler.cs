using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.QueryHandlers
{
	public class GetBillByIdHandler : IRequestHandler<GetBillById, Bill>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBillByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Bill> Handle(GetBillById request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new NullReferenceException("Request is null");
            }
            var bill = await _unitOfWork.BillRepository.GetById(request.Id);
            if (bill == null)
            {
                throw new NullReferenceException("No bill found");
            }
            return bill;
        }
    }
}
