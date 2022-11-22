using DoctorAppointment.Application.Commands;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.CommandHandlers
{
    public class InsertMedicalVisitHandler : IRequestHandler<InsertMedicalVisit, MedicalVisit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsertMedicalVisitHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MedicalVisit> Handle(InsertMedicalVisit request, CancellationToken cancellationToken)
        {
            var medicalVisit = new MedicalVisit
            {
                Id = request.Id,
                Date = request.Date,
                Description = request.Description,
                DoctorId = request.DoctorId,
                PatientId = request.PatientId
            };

            await _unitOfWork.MedicalVisitRepository.Insert(medicalVisit);
            await _unitOfWork.Save();

            return medicalVisit;
        }
    }
}
