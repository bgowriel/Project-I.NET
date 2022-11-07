using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IMedicineService
    {
        public Guid AddMedicine(MedicineRequest medicine);
    }
}
