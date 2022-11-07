using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Guid AddUser(UserRequest user);
    }
}
