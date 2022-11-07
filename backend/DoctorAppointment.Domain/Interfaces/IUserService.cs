using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IUserService
    {
        public Guid AddUser(UserRequest user);
    }
}
