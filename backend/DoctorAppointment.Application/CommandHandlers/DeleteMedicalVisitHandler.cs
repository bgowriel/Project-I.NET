using DoctorAppointment.Application.Commands;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.CommandHandlers
{
    public class DeleteMedicalVisitHandler : IRequestHandler<DeleteMedicalVisit, MedicalVisit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMedicalVisitHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MedicalVisit> Handle(DeleteMedicalVisit request, CancellationToken cancellationToken)
        {
            var medicalVisit = await _unitOfWork.MedicalVisitRepository.GetById(request.Id);

            if (medicalVisit == null)
            {
                return null;
            }

            _unitOfWork.MedicalVisitRepository.Delete(medicalVisit);
            await _unitOfWork.Save();

            return medicalVisit;
        }
    }
}
