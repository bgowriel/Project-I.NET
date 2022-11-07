namespace DoctorAppointment.Domain.Models.Request
{
    public class ServiceProvided
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Description { get; private set; }
        public Doctor Doctor { get; private set; }
        
    }
}
