using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DoctorAppointment.Application.QueryHandlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserById, User>
    {
        private readonly UserManager<User> _userManager;

        public GetUserByIdHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                return new User();
            }

            var user = await _userManager.FindByIdAsync(request.Id);
            if (user != null)
            {
                return user;
            }
            else
            {
                return new User();
            }
        }
    }
}
