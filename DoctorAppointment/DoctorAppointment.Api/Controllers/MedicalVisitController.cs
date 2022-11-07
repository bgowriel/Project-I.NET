using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.Api.Controllers
{
    public class MedicalVisitController : ControllerBase
    {
        private readonly IMedicalVisitService medicalVisitService;

        public MedicalVisitController(IMedicalVisitService medicalVisitService)
        {
            this.medicalVisitService = medicalVisitService;
        }

        [HttpPost("medicalVisit")]
        public ActionResult AddMedicalVisit(MedicalVisit medicalVisit)
        {
            var result = medicalVisitService.AddMedicalVisit(medicalVisit);
            return Ok(result);
        }
    }
}
