namespace SigmaAssignment.Data.DTO
{
    public class BaseResponse<T> where T : class
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public BaseResponse()
        {
        }

        public BaseResponse(T data, bool success, string message, List<string> errors = null)
        {
            Data = data;
            Success = success;
            Message = message;
            Errors = errors;
        }
    }
}
