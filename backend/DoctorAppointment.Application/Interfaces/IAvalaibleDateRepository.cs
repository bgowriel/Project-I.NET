using DoctorAppointment.Domain.Models;

namespace DoctorAppointment.Application.Interfaces
{
    public interface IAvalaibleDateRepository
    {
        Task<AvalaibleDate?> GetById(Guid id);
        Task<List<AvalaibleDate>?> GetAll();
        Task Insert(AvalaibleDate avalaibleDate);
    }
}
