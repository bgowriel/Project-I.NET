using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.Api.Controllers
{
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        [HttpPost("appointment")]
        public ActionResult AddAppointment(AppointmentRequest appointment)
        {
            var result = appointmentService.AddApointment(appointment);
            return Ok(result);
        }
    }
}
