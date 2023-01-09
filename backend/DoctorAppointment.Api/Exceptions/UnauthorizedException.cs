using System.Runtime.Serialization;

namespace DoctorAppointment.Api.Exceptions
{
    [Serializable]
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base("UnauthorizedException " + message)
        {
        }

        protected UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
