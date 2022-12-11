namespace DoctorAppointment.Api.Dto
{
    public class AssignDoctorDto
    {
		private string? doctorId;
		private Guid officeId;

		public string? DoctorId { get => doctorId; set => doctorId = value; }
		public Guid OfficeId { get => officeId; set => officeId = value; }
	}
}
