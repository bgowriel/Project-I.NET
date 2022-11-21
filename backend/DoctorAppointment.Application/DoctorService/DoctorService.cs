using DoctorAppointment.Domain.Helpers;
using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Response;

namespace DoctorAppointment.Application.DoctorService
{
    public class DoctorService : IDoctorService
    {
        private IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
   
        public List<DoctorResponse> GetAll()
        {
            return _doctorRepository.GetAll();
        }

        public Result<DoctorResponse> AddDoctor(DoctorResponse doctor)
        {
            _doctorRepository.AddDoctor(doctor);
            _doctorRepository.Save();
            return Result<DoctorResponse>.Success(doctor);
        }
    }
}