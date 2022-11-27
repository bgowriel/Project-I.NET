using AutoMapper;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoctorAppointment.Api.Controllers
{
	[Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly UserManager<User> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

		private readonly IMediator _mediator;

		private readonly IMapper _mapper;

		public UsersController(IConfiguration configuration,
                               UserManager<User> userManager,
                               RoleManager<IdentityRole> roleManager,
							   IMediator mediator,
							   IMapper mapper
							   )
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var roleExists = await _roleManager.RoleExistsAsync("Admin");
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            
            roleExists = await _roleManager.RoleExistsAsync("Patient");
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("Patient"));
            }

            roleExists = await _roleManager.RoleExistsAsync("Doctor");
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("Doctor"));
            }

            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
            {
                return BadRequest(new { message = "User already exists!" });
            }

            User user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                Role = model.Role,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new { message = "User creation failed! Please check user details and try again." });
            }

            await _userManager.AddToRoleAsync(user, model.Role);
            /*if (model.Email == "bogdanvflorea@gmail.com")
              {
                  await _userManager.AddToRoleAsync(user, "admin");
              }*/

            return CreatedAtAction(nameof(Register), user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim("userId", user.Id),
                    new Claim("email", user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],//"https://localhost:7147", backend
                    audience: _configuration["JWT:ValidAudience"],//"http://localhost:4200", frontend
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    //userRole = userRoles.FirstOrDefault(),
                    //userName = user.UserName
                });
            }

            return Unauthorized();
        }

        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("getUser")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return Ok(user);
        }

        [HttpPost]
        [Route("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest(new { message = "User does not exist!" });
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                return BadRequest(new { message = "Role does not exist!" });
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Role assignment failed!" });
            }

            return Ok(new { message = "Role assigned successfully!" });
        }

		[HttpPut]
		[Route("assign-doctor-to-office")]
		public async Task<IActionResult> AssignDoctorToOffice([FromQuery] string doctorId, [FromQuery] Guid officeId)
		{
			var doctor = await _userManager.FindByIdAsync(doctorId);
			if (doctor == null)
			{
				return BadRequest(new { message = "Doctor does not exist!" });
			}
			var office = await _mediator.Send(new GetOfficeById() { Id = officeId });
			if (office == null)
			{
				return NotFound();
			}

            doctor.OfficeId = office.Id;
            var result = await _userManager.UpdateAsync(doctor);
			if (!result.Succeeded)
			{
				return BadRequest(new { message = "Office assignment failed!" });
			}

			return Ok(new { message = "Office assigned successfully!" });
		}
	}
}
