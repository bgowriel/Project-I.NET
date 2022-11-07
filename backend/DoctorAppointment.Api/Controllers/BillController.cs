using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.Api.Controllers
{
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;
        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpPost("bill")]
        public ActionResult AddBill(BillRequest bill)
        {
            var result = _billService.AddBill(bill);
            return Ok(result);
        }
    }
}
