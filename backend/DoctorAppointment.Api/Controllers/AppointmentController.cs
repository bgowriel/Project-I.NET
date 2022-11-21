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
        public ActionResult AddAppointment([FromBody] AppointmentRequest appointment)
        {
            var newAppointment = new AppointmentResponse(appointment.Name, appointment.Date, appointment.DoctorId, appointment.PatientId, appointment.ServiceProvidedId);
            var result = appointmentService.AddApointment(newAppointment);

            if (result.IsSuccess)
             return Created(nameof(Get), newAppointment);
            else
             return BadRequest(result);
        }
    }
}
