using DoctorAppointment.Domain.Models.Request;
using DoctorAppointment.Domain.Models.Response;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IAdminUserRepository
    {
        public Guid AddAdminUser(AdminRequest adminUser);

        public AdminResponse GetAdminUser(Guid guid);
        
        public AdminResponse UpdateAdminUser(Guid guid, AdminRequest adminUser);

        public bool DeleteAdminUser(Guid guid);
    }
}
