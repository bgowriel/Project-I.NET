namespace DoctorAppointment.Api.Dto
{
    public class AssignDoctorDto
    {
        public string? doctorId { get; set; }

        public Guid? officeId { get; set; }
    }
}
