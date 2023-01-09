using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            if (_userManager is null) return new List<User>();
            
            var users = await _userManager.Users.Take(1000).ToListAsync(cancellationToken: cancellationToken);
            return users;
        }
    }
}
