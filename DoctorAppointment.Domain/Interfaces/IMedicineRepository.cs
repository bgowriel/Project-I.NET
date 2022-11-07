using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IMedicineRepository
    {
        public Guid AddMedicine(MedicineRequest medicine);
    }
}
