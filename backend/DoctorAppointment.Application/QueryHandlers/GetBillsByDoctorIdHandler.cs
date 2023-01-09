using DoctorAppointment.Application.Exceptions;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.QueryHandlers
{
    public class GetBillsByDoctorIdHandler : IRequestHandler<GetBillsByDoctorId, List<Bill>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBillsByDoctorIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Bill>> Handle(GetBillsByDoctorId request, CancellationToken cancellationToken)
        {
            if (request.DoctorId == null)
            {
                throw new NotFoundException("PatientId is null");
            }
            var bills = await _unitOfWork.BillRepository.GetByDoctorId(request.DoctorId);
            if (bills == null)
            {
                return new List<Bill>();
            }
            return bills;
        }


    }
}
