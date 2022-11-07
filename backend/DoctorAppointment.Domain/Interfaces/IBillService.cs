using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IBillService
    {
        public Guid AddBill(BillRequest bill);
    }
}
