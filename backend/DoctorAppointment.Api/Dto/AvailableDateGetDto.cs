namespace DoctorAppointment.Api.Dto
{
    public class AvailableDateGetDto
    {
            public Guid Id { get; set; }

            public DateTime Date { get; set; }

            public bool Free { get; set; }
    }
}
