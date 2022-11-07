using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.DataAccess
{
    public class MedicineRepository : IMedicineRepository
    {
        public Guid AddMedicine(MedicineRequest medicine)
        {
            return new Guid();
        }
    }
}
