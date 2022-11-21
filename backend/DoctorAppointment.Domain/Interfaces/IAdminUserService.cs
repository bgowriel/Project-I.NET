using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IAdminUserService
    {
        public Guid AddAdminUser(AdminRequest adminUser);
    }
}
