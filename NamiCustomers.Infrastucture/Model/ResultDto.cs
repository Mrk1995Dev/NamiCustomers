namespace NamiCustomers.Infrastucture.Model
{
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


    public class ResultDto<T> where T : class
    {
        public ResultDto(string message, bool isSuccess, T data)
        {
            Message = message;
            IsSuccess = isSuccess;
            Data = data;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
