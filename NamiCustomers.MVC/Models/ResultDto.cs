namespace NamiCustomers.MVC.Models
{
    public class ResultDto<T> where T : class
    {
        public ResultDto(string message, bool isSuccess, T data)
        {
            Message = message;
            IsSuccess = isSuccess;
            Data = data;
        }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
    }
    public class ResultDto
    {
        public ResultDto(string message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }

}