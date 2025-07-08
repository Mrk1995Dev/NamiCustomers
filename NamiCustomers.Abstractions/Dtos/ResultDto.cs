namespace NamiCustomers.Abstractions.Dtos
{
    public class ResultDto<T> where T : class
    {
        public ResultDto(string? Message, bool Succeeded, T Data, ApiErrorResponse Errors = null)
        {
            this.Message = Message;
            this.Succeeded = Succeeded;
            this.Data = Data;
            this.Errors = Errors ?? new ApiErrorResponse(new List<ApiError>());
        }

        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; }
        public ApiErrorResponse Errors { get; set; }
    }
    public class ResultDto
    {
        public ResultDto(string message, bool Succeeded)
        {
            Message = message;
            this.Succeeded = Succeeded;
        }
        public string Message { get; set; }
        public bool Succeeded { get; set; }
    }
 
    public record ApiErrorResponse(List<ApiError> Errors);

    public record ApiError(string Code, string Description);
}