namespace DoctorAppointment.Api.Exceptions
{
    public class JWTException : Exception
    {
        public JWTException(string message) : base(message)
        {
        }
    }
}
