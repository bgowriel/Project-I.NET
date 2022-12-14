using System.Runtime.Serialization;

namespace DoctorAppointment.Api.Exceptions
{
    [Serializable]
    public class AssemblyException : Exception
    {
        public AssemblyException(string message) : base("AssemblyException " + message)
        {
        }

        protected AssemblyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
