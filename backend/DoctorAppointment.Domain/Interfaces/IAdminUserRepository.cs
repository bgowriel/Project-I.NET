using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IAdminUserRepository
    {
        public Guid AddAdminUser(AdminRequest adminUser);
    }
}
