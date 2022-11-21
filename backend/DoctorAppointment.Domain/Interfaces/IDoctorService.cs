using DoctorAppointment.Domain.Helpers;
using DoctorAppointment.Domain.Models.Request;
using DoctorAppointment.Domain.Models.Response;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IDoctorService
    {
        public Result<DoctorResponse> AddDoctor(DoctorResponse doctor);

        public List<DoctorResponse> GetAll();
    }
}
