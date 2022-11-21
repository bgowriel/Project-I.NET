using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IAdminUserService _adminUserService;

        public AdminUserController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        [HttpPost("admin")]
        public ActionResult AddAdminUser([FromBody] AdminRequest adminUser)
        {
            var result = _adminUserService.AddAdminUser(adminUser);

            return Ok(result);
        }

        [HttpGet("admin/{guid}")]
        public ActionResult GetAdminUser(Guid guid)
        {
            var result = _adminUserService.GetAdminUser(guid);

            return Ok(result);
        }

        [HttpPut("admin/{guid}")]
        public ActionResult UpdateAdminUser(Guid guid, [FromBody] AdminRequest adminUser)
        {
            var result = _adminUserService.UpdateAdminUser(guid, adminUser);

            return Ok(result);
        }

        [HttpDelete("admin/{guid}")]
        public ActionResult DeleteAdminUser(Guid guid)
        {
            var result = _adminUserService.DeleteAdminUser(guid);

            return Ok(result);
        }
    }
}
