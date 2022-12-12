using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.QueryHandlers
{
	public class GetMedicalVisitByIdHandler : IRequestHandler<GetMedicalVisitById, MedicalVisit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMedicalVisitByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MedicalVisit> Handle(GetMedicalVisitById request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new NullReferenceException("Id cannot be null");
            }
            var medicalVisit = await _unitOfWork.MedicalVisitRepository.GetById(request.Id);
            if (medicalVisit == null)
            {
                throw new NullReferenceException("No medical visit found");
            }
            return medicalVisit;
        }
    }
}
