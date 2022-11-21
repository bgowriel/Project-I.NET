using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.DataAccess
{
    public class AdminUserRepository : IAdminUserRepository
    {
        public Guid AddAdminUser(AdminRequest adminUser)
        {
            return new Guid();
        }
    }
}
