using DoctorAppointment.Domain.Models.Request;

namespace DoctorAppointment.Domain.Models.Response
{
    public class AppointmentResponse
    {
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public DateTime Date { get; private set; }

        public Guid DoctorId { get; private set; }

        public DoctorResponse Doctor { get; private set; }

        public Guid UserId { get; private set; }

        public UserResponse User { get; private set; }
    }
}
