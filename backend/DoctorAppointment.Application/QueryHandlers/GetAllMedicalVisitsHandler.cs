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
    public class GetAllMedicalVisitsHandler : IRequestHandler<GetAllMedicalVisits, List<MedicalVisit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllMedicalVisitsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<MedicalVisit>> Handle(GetAllMedicalVisits request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.MedicalVisitRepository.GetAll();
        }
    }
}
