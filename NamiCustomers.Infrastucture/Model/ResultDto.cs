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
        public ResultDto(string? Message, bool IsSuccess, T Data, ApiErrorResponse Errors=null)
        {
            this.Message = Message;
            this.Issuccess = IsSuccess;
            this.Data = Data;
            this.Errors = Errors??new ApiErrorResponse(new List<ApiError>());
        }

        public bool Issuccess { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; }
        public ApiErrorResponse Errors { get; set; }
    }


}
