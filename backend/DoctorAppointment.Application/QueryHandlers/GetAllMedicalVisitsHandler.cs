using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.QueryHandlers
{
	public class GetAllMedicalVisitsHandler : IRequestHandler<GetAllMedicalVisits, List<MedicalVisit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllMedicalVisitsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<MedicalVisit>> Handle(GetAllMedicalVisits request, CancellationToken cancellationToken)
        {
            var medicalVisits = await _unitOfWork.MedicalVisitRepository.GetAll();
            medicalVisits ??= new List<MedicalVisit>();
            return medicalVisits;
        }
    }
}
