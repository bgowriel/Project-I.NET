namespace DoctorAppointment.Api.Exceptions
{
    public class MediatRException : Exception
    {
        public MediatRException(string message) : base(message)
        {
        }
    }
}
