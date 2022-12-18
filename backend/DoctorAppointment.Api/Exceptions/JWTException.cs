using System.Runtime.Serialization;

namespace DoctorAppointment.Api.Exceptions
{
    [Serializable]
    public class JwtException : Exception
    {
        public JwtException(string message) : base(message)
        {
        }

        protected JwtException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
