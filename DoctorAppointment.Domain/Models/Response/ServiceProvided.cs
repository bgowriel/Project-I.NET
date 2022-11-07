namespace DoctorAppointment.Domain.Models.Response
{
    public class ServiceProvided
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Description { get; private set; }
        public Doctor Doctor { get; private set; }
        
    }
}
