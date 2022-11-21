namespace DoctorAppointment.Domain.Models.Response
{
    public class ServiceProvidedResponse
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }
        
        public string Type { get; private set; }
        
        public string Description { get; private set; }
        
        public DoctorResponse Doctor { get; private set; }        
    }
}
