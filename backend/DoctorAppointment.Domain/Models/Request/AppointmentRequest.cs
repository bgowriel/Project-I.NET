using DoctorAppointment.Domain.Models.Response;

namespace DoctorAppointment.Domain.Models.Request
{
    public class AppointmentRequest
    {
        public string Name { get; private set; }

        public DateTime Date { get; private set; }

        public Guid DoctorId { get; private set; }

        public DoctorResponse Doctor { get; private set; }

        public Guid UserId { get; private set; }

        public UserResponse User { get; private set; }
    }
}
