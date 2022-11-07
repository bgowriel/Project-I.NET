using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.Api.Controllers
{
    public class AdminUserController : ControllerBase
    {
        private readonly IAdminUserService _adminUserService;

        public AdminUserController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        [HttpPost("admin")]
        public ActionResult AddAdminUser(Admin adminUser)
        {
            var result = _adminUserService.AddAdminUser(adminUser);

            return Ok(result);
        }
    }
}
