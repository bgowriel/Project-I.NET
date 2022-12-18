using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.DataAccess.Repositories
{
	public class OfficeRepository : IOfficeRepository
	{

		private readonly DatabaseContext _databaseContext;

		public OfficeRepository(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}
	
		public async Task<Office?> GetById(Guid id)
		{
			var office = await _databaseContext.Offices.SingleOrDefaultAsync(o => o.Id == id);
            return office;
        }
		public async Task<List<Office>?> GetAll()
		{
			return await _databaseContext.Offices.Take(100).ToListAsync();
		}

		public async Task<List<User>?> GetAllDoctors(Guid id)
		{
            var doctors = await _databaseContext.Offices.Where(o => o.Id == id)
                .Include(o => o.Doctors).Select(o => o.Doctors).SingleAsync();

            return doctors;

        }

		public async Task Insert(Office entity)
		{
			await _databaseContext.Offices.AddAsync(entity);
		}

		public void Update(Office entity)
		{
			_databaseContext.Offices.Update(entity);
		}
		public void Delete(Office entity)
		{
			_databaseContext.Offices.Remove(entity);
		}
	}
}
