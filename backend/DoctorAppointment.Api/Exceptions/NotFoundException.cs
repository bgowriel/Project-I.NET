using System.Runtime.Serialization;

namespace DoctorAppointment.Api.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base("NotFoundException " + message)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
