using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Application.QueryHandlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsers, List<User>>
    {
        private readonly UserManager<User> _userManager;

        public GetAllUsersHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<User>> Handle(GetAllUsers request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.Take(1000).ToListAsync();
            return users;
        }
    }
}
