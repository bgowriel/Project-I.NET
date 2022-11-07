using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IBillRepository
    {
        public Guid AddBill(Bill bill);
    }
}
