using DoctorAppointment.Api.Services;
using DoctorAppointment.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.Api.Controllers
{
    [Route("api/seed")]
    [ApiController]
    public class SeedDBController : ControllerBase
    {
        private readonly SeedDBService _seedDBService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public SeedDBController(SeedDBService seedDBService, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _seedDBService = seedDBService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> SeedDB()
        {
            await _seedDBService.Seed(_userManager, _roleManager);
            return Ok();
        }
    }
}
