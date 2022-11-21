using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Models.Response;

namespace DoctorAppointment.DataAccess
{
    public class AppointmentRepository:IAppointmentRepository
    {
        private readonly IDatabaseContext context;

        public AppointmentRepository(IDatabaseContext context)
        {
            this.context = context;
        }
        public void AddApointment(AppointmentResponse appointment)
        {
            context.Appointments.Add(appointment);
        }

        public List<AppointmentResponse> GetAll()
        {
            return context.Appointments.ToList();
        }

        public void Save()
        {
            context.Save();
        }
    }
}
