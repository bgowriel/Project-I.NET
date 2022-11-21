using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.DataAccess
{
    public class DoctorRepository : IDoctorRepository
    {
        public Guid AddDoctor(DoctorRequest doctor)
        {
            return new Guid();
        }
    }
}
