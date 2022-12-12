﻿using AutoMapper;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Api.Validators;
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

            //if role different from Admin, Patient or Doctor return BadRequest
            if (model.Role != "Admin" && model.Role != "Patient" && model.Role != "Doctor")
            {
                return BadRequest(new { message = "Role must be Admin, Patient or Doctor" });
            }

			if (model.Email == null || model.Password == null)
			{
				return BadRequest(new { message = "Email and Password are required" });
			}

			var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
            {
                return BadRequest(new { message = "User already exists!" });
            }

            //validate user
            var validator = new RegisterModelValidator();
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            User user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                Role = model.Role,
                PhoneNumber = model.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new { message = "User creation failed! Please check user details and try again." });
            }

            await _userManager.AddToRoleAsync(user, model.Role);
            if (model.Email == "silviu@gmail.com")
            {
                await _userManager.AddToRoleAsync(user, "admin");
            }

            return CreatedAtAction(nameof(Register), user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            //validate user
            var validator = new LoginModelValidator();
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (model.Email == null || model.Password == null)
            {
                return BadRequest(new { message = "Email and Password are required" });
            }

            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
				if (user.Email == null)
				{
					return BadRequest(new { message = "Email and Password are required" });
				}
				var authClaims = new List<Claim>
                {
                    new Claim("userId", user.Id),
                    new Claim("email",  value : user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim("role", userRole));
                }
                var _config = _configuration["JWT:Secret"];
				
				if (_config == null)
                {
					return BadRequest(new { message = "JWT:Secret not found" });
				}

				var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config));

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
        //[Route("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            if (user == null)
            {
                return NotFound();
            }
            
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
		[Route("assign-doctor-to-office/{doctorId}/{officeId}")]
		public async Task<IActionResult> AssignDoctorToOffice([FromRoute] string doctorId, [FromRoute] Guid officeId)
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

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "User deletion failed!" });
            }

            return Ok(new { message = "User deleted successfully!" });
        }

        [HttpPut]
        [Route("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var userExists = await _userManager.FindByIdAsync(user.Id);
            if (userExists == null)
            {
                return NotFound();
            }

            userExists.FirstName = user.FirstName;
            userExists.LastName = user.LastName;
            userExists.Email = user.Email;
            userExists.UserName = user.Email;
            userExists.PhoneNumber = user.PhoneNumber;
            userExists.Role = user.Role;

            var result = await _userManager.UpdateAsync(userExists);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "User update failed!" });
            }

            return Ok(new { message = "User updated successfully!" });
        }
    }
}
