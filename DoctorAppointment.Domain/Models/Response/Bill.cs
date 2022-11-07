namespace DoctorAppointment.Domain.Models.Response
{
    public class Bill
    {
        public int Id { get; private set; }
        public string? Code { get; private set; }
        public User? Patient { get; private set; }

        public Doctor? Doctor { get; private set; }
        public float? Total { get; private set; }
        public DateTime? StatementDate { get; private set; }
        public DateTime? DueDate { get; private set; }

        //List<ServiceProvided> services;
    }
}
