using DoctorAppointment.Domain.Helpers;
using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Response;

namespace DoctorAppointment.DataAccess
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IDatabaseContext context;

        public DoctorRepository(IDatabaseContext context)
        {
            this.context = context;
        }
        public Result AddDoctor(DoctorResponse doctor)
        {
            context.Doctors.Add(doctor);
            return Result.Success();
        }

        public List<DoctorResponse> GetAll()
        {
            return context.Doctors.ToList();
        }
        public void Save()
        {
            context.Save();
        }
    }
}
