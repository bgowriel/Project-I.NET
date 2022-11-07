using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.DataAccess
{
    public class BillRepository : IBillRepository
    {
        public Guid AddBill(Bill bill)
        {
            return new Guid();
        }
    }
}
