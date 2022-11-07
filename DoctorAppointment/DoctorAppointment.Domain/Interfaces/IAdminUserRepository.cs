using DoctorAppointment.Domain.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IAdminUserRepository
    {
        public Guid AddAdminUser(AdminUser adminUser);
    }
}
