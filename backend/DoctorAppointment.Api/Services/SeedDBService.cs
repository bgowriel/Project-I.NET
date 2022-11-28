using DoctorAppointment.Application;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace DoctorAppointment.Api.Services
{
    public class SeedDBService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SeedDBService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var generator = new Generator(_unitOfWork, userManager, roleManager);
            await generator.Generate();
        }
    }
}
