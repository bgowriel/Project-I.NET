using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.QueryHandlers
{
	public class GetAllBillsHandler : IRequestHandler<GetAllBills, List<Bill>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBillsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Bill>> Handle(GetAllBills request, CancellationToken cancellationToken)
        {
            var bills = await _unitOfWork.BillRepository.GetAll();
            bills ??= new List<Bill>();
            return bills;
        }
    }
}
