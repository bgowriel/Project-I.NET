namespace DoctorAppointment.Domain.Models.Request
{
    public class ServiceProvidedRequest
    {
        public string Name { get; private set; }
        
        public string Type { get; private set; }
        
        public string Description { get; private set; }
        
        public DoctorRequest Doctor { get; private set; }        
    }
}
