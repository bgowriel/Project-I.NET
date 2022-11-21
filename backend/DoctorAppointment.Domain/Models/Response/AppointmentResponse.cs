namespace DoctorAppointment.Domain.Models.Response
{
    public class AppointmentResponse
    {
        public AppointmentResponse(string name, DateTime date, Guid doctorId, Guid patientId, Guid serviceProvidedId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Date = date;
            DoctorId = doctorId;
            PatientId = patientId;
            ServiceProvidedId = serviceProvidedId;

        }
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public DateTime Date { get; private set; }

        public Guid DoctorId { get; private set; }

        public Guid PatientId { get; private set; }

        public Guid ServiceProvidedId { get; set; }

    }
}
