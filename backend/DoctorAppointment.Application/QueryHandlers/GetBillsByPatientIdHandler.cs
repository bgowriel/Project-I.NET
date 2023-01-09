using DoctorAppointment.Application.Exceptions;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.QueryHandlers
{
    public class GetBillsByPatientIdHandler : IRequestHandler<GetBillsByPatientId, List<Bill>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBillsByPatientIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Bill>> Handle(GetBillsByPatientId request, CancellationToken cancellationToken)
        {
            if (request.PatientId == null)
            {
                throw new NotFoundException("PatientId is null");
            }
            var bills = await _unitOfWork.BillRepository.GetByPatientId(request.PatientId);
            if (bills == null)
            {
                return new List<Bill>();
            }
            return bills;
        }

       
    }
    
    
}
