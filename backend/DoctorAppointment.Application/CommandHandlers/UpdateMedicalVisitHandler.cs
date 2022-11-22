using DoctorAppointment.Application.Commands;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.CommandHandlers
{
    public class UpdateMedicalVisitHandler : IRequestHandler<UpdateMedicalVisit, MedicalVisit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMedicalVisitHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MedicalVisit> Handle(UpdateMedicalVisit request, CancellationToken cancellationToken)
        {
            var toUpdate = new MedicalVisit
            {
                Id = request.Id,
                Date = request.Date,
                Description = request.Description,
                DoctorId = request.DoctorId,
                PatientId = request.PatientId
            };

            _unitOfWork.MedicalVisitRepository.Update(toUpdate);
            await _unitOfWork.Save();

            return toUpdate;
        }
    }
}
