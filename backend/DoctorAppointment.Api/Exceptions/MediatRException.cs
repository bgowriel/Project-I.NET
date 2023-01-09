using System.Runtime.Serialization;

namespace DoctorAppointment.Api.Exceptions
{
    [Serializable]
    public class MediatRException : Exception
    {
        public MediatRException(string message) : base("MediatRException " + message)
        {
        }

        protected MediatRException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
