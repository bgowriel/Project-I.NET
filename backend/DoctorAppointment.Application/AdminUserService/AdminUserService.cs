using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;
using DoctorAppointment.Domain.Models.Response;

namespace DoctorAppointment.Application.AdminUserService
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IAdminUserRepository _adminUserRepository;

        public AdminUserService(IAdminUserRepository adminUserRepository)
        {
            _adminUserRepository = adminUserRepository;
        }

        public Guid AddAdminUser(AdminRequest adminUser)
        {
            return _adminUserRepository.AddAdminUser(adminUser);
        }


        public AdminResponse GetAdminUser(Guid guid)
        {
            return _adminUserRepository.GetAdminUser(guid);
        }

        public AdminResponse UpdateAdminUser(Guid guid, AdminRequest adminUser)
        {
            return _adminUserRepository.UpdateAdminUser(guid, adminUser);
        }
        
        public bool DeleteAdminUser(Guid guid)
        {
            return _adminUserRepository.DeleteAdminUser(guid);
        }
    }
}