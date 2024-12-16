namespace SigmaAssignment.Data.DTO
{
    public class BaseResponse<T> where T : class
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Errors { get; set; }

        public BaseResponse(T data, int statusCode, string message, object errors = null)
        {
            Data = data;
            StatusCode = statusCode;
            Message = message;
            Errors = errors;
        }
    }
}
