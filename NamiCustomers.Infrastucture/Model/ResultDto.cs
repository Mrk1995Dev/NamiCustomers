using Newtonsoft.Json;
using System.Text.Json.Serialization;

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
        public ResultDto(string? message, bool isSuccess, T data)
        {
            this.message = message;
            this.issuccess = isSuccess;
            this.Data = data;
        }

        public bool issuccess { get; set; }
        public string? message { get; set; }
        public T Data { get; set; }
    }
}
