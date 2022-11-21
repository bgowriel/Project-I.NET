using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.Api.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("user")]
        public ActionResult AddUser(UserRequest user)
        {
            var result = _userService.AddUser(user);
            return Ok(result);
        }

        public ActionResult AddDoctorUser()
        {
            return Ok();
        }
        
    }
}
