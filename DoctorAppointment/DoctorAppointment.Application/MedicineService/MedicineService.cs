using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Application.MedicineService
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;
        public MedicineService(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }
        public Guid AddMedicine(Medicine medicine)
        {
            return _medicineRepository.AddMedicine(medicine);
        }
    }
}
