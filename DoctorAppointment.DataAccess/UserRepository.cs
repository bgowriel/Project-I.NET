using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.DataAccess
{
    public class UserRepository : IUserRepository
    {
        public Guid AddUser(User user)
        {
            return new Guid();
        }
    }
}
