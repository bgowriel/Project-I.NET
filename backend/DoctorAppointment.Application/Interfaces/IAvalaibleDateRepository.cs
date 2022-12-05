using DoctorAppointment.Domain.Models;

namespace DoctorAppointment.Application.Interfaces
{
    public interface IAvalaibleDateRepository
    {
        Task<AvailableDate?> GetById(Guid id);
        Task<List<AvailableDate>?> GetAll();
        Task Insert(AvailableDate avalaibleDate);
    }
}
