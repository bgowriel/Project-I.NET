using DoctorAppointment.Application.AppointmentService;
using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;
using DoctorAppointment.Domain.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;

namespace DoctorAppointment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_doctorService.GetAll());
        }

        [HttpPost]
        public ActionResult AddDoctor([FromBody] DoctorRequest doctor)
        {
            var newDoctor = new DoctorResponse(doctor.Name, doctor.Email, doctor.Phone, doctor.Address,
                doctor.Specialization, doctor.Description, doctor.Image, doctor.Role, doctor.Token,
                doctor.Status);
            var result = _doctorService.AddDoctor(newDoctor);

            if (result.IsSuccess)
                return Created(nameof(Get), newDoctor);
            else
                return BadRequest(result);
        }
    }
}
