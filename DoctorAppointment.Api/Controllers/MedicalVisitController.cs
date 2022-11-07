using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.Api.Controllers
{
    public class MedicalVisitController : ControllerBase
    {
        private readonly IMedicalVisitService _medicalVisitService;

        public MedicalVisitController(IMedicalVisitService medicalVisitService)
        {
            _medicalVisitService = medicalVisitService;
        }

        [HttpPost("medicalVisit")]
        public ActionResult AddMedicalVisit(MedicalVisitRequest medicalVisit)
        {
            var result = _medicalVisitService.AddMedicalVisit(medicalVisit);
            return Ok(result);
        }
    }
}
