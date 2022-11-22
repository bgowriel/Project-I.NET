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
    public class GetMedicalVisitByIdHandler : IRequestHandler<GetMedicalVisitById, MedicalVisit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMedicalVisitByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MedicalVisit> Handle(GetMedicalVisitById request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.MedicalVisitRepository.GetById(request.Id);
        }
    }
}
