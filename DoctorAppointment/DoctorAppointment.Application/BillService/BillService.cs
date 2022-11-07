using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Application.BillService
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;

        public BillService(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public Guid AddBill(Bill bill)
        {
            return _billRepository.AddBill(bill);
        }
    }
}
