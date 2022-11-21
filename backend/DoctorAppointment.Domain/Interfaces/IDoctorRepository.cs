using DoctorAppointment.Domain.Helpers;
using DoctorAppointment.Domain.Models.Response;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IDoctorRepository
    {
        public Result AddDoctor(DoctorResponse doctor);

        public List<DoctorResponse> GetAll();

        public void Save();
    }
}
