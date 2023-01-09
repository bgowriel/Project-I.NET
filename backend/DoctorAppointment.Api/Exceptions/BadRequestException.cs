using System.Runtime.Serialization;

namespace DoctorAppointment.Api.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base("BadRequestException " + message)
        {
        }

        protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
