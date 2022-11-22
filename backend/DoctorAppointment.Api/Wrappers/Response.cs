namespace DoctorAppointment.Api.Wrappers
{
    public class Response<T>
    {
        public T? Data { get; set; }

        public string? Message { get; set; }

        public string[]? Errors { get; set; }

        public bool Success { get; set; }

        public int StatusCode { get; set; }

        public Response() { }

        public Response(T data)
        {
            Success = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
            StatusCode = 200;
        }
    }
}
