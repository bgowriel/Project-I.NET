using DoctorAppointment.Api.Dtos;
using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;
using DoctorAppointment.Domain.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(appointmentService.GetAll());
        }

        [HttpPost]
        public ActionResult AddAppointment([FromBody] CreateAppointmentDto dto)
        {
            var newAppointment = new AppointmentResponse(dto.Name, dto.Date, dto.DoctorId, dto.PatientId, dto.ServiceProvidedId);
            appointmentService.AddApointment(newAppointment);
            return Created(nameof(Get), newAppointment);
        }
    }
}
