using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Application.DoctorService
{
    public class DoctorService : IDoctorService
    {
        private IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public Guid AddDoctor(Doctor doctor)
        {
            return _doctorRepository.AddDoctor(doctor);
        }
    }
}