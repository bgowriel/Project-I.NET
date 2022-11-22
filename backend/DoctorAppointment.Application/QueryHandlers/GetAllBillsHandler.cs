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
    public class GetAllBillsHandler : IRequestHandler<GetAllBills, List<Bill>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBillsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Bill>> Handle(GetAllBills request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BillRepository.GetAll();
        }
    }
}
