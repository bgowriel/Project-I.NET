using DoctorAppointment.Domain.Models.Response;

namespace DoctorAppointment.Domain.Models.Request
{
    public class ServiceProvidedRequest
    {
        public string Name { get; private set; }
        
        public string Type { get; private set; }
        
        public string Description { get; private set; }

        public Guid DoctorId { get; private set; }

        public DoctorResponse Doctor { get; private set; }        
    }
}
