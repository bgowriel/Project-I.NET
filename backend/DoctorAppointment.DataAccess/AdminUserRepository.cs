using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;
using DoctorAppointment.Domain.Models.Response;

namespace DoctorAppointment.DataAccess
{
    public class AdminUserRepository : IAdminUserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public AdminUserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Guid AddAdminUser(AdminRequest adminUser)
        {
            var adminUserEntity = new AdminResponse
            {
                Id = Guid.NewGuid(),
                FirstName = adminUser.FirstName,
                LastName = adminUser.LastName,
                Email = adminUser.Email,
                Password = adminUser.Password,
                Phone = adminUser.Phone,
                Address = adminUser.Address,
                Role = adminUser.Role,
            };

            _databaseContext.Admins.Add(adminUserEntity);
            _databaseContext.Save();

            return adminUserEntity.Id;
        }

        public AdminResponse GetAdminUser(Guid guid)
        {
            var adminUserEntity = _databaseContext.Admins.FirstOrDefault(x => x.Id == guid);

            if (adminUserEntity == null)
            {
                return null;
            }

            var adminUser = new AdminResponse
            {
                Id = adminUserEntity.Id,
                FirstName = adminUserEntity.FirstName,
                LastName = adminUserEntity.LastName,
                Email = adminUserEntity.Email,
                Password = adminUserEntity.Password,
                Phone = adminUserEntity.Phone,
                Address = adminUserEntity.Address,
                Role = adminUserEntity.Role,
            };

            return adminUser;
        }

        public AdminResponse UpdateAdminUser(Guid guid, AdminRequest adminUser)
        {
            var adminUserEntity = _databaseContext.Admins.FirstOrDefault(x => x.Id == guid);

            if (adminUserEntity == null)
            {
                return null;
            }

            adminUserEntity.FirstName = adminUser.FirstName;
            adminUserEntity.LastName = adminUser.LastName;
            adminUserEntity.Email = adminUser.Email;
            adminUserEntity.Password = adminUser.Password;
            adminUserEntity.Phone = adminUser.Phone;
            adminUserEntity.Address = adminUser.Address;
            adminUserEntity.Role = adminUser.Role;

            _databaseContext.Save();

            return adminUserEntity;
        }

        public bool DeleteAdminUser(Guid guid)
        {
            var adminUserEntity = _databaseContext.Admins.FirstOrDefault(x => x.Id == guid);

            if (adminUserEntity == null)
            {
                return false;
            }

            _databaseContext.Admins.Remove(adminUserEntity);
            _databaseContext.Save();

            return true;
        }
    }
}
