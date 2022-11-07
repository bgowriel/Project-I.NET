using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IMedicalVisitService
    {
        public Guid AddMedicalVisit(MedicalVisit medicalVisit);
    }
}

//using DoctorAppointment.Domain.Models.Request;

//namespace DoctorAppointment.Domain.Interfaces
//{
//    public interface IUserService
//    {
//        public Guid AddUser(User user);
//    }
//}
