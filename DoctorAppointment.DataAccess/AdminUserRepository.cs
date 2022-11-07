using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.DataAccess
{
    public class AdminUserRepository : IAdminUserRepository
    {
        public Guid AddAdminUser(AdminUser adminUser)
        {
            return new Guid();
        }
    }
}
