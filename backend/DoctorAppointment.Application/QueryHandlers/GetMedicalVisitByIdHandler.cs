using DoctorAppointment.Application.Exceptions;
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
            if (request == null)
            {
                throw new NotFoundException("MedicalVisit.Id is null");
            }
            var medicalVisit = await _unitOfWork.MedicalVisitRepository.GetById(request.Id);
            if (medicalVisit == null)
            {
                throw new NotFoundException("No medical visit with given Id was found");
            }
            return medicalVisit;
        }
    }
}
