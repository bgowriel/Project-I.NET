namespace DoctorAppointment.Api.Dto
{
    public class AssignDoctorDto
    {
		private string? doctorId;
		private Guid? officeId;

		public string? DoctorId
		{
			get { return doctorId; }
			set { doctorId = value; }
		}
		public Guid? OfficeId {
			get { return officeId; }
			set { officeId = value; }
		}
	}
}
