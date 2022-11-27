using DoctorAppointment.Domain.Models;

namespace DoctorAppointment.Application.Interfaces
{
	public interface IOfficeRepository
	{
		Task<Office> GetById(Guid id);

		Task<List<Office>> GetAll();

		Task<List<User>> GetAllDoctors(Guid id);

		Task Insert(Office entity);

		void Update(Office entity);

		void Delete(Office entity);
	}
}
