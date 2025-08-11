namespace NamiCustomers.Abstractions.Dtos;
public class ResultDto<T> where T : class
{
    public ResultDto(string? message, bool succeeded, T data = null, List<string> errors = null)
    {
        this.Message = message;
        this.Succeeded = succeeded;
        this.Data = data;
        this.Errors = errors;

    }

    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public List<string> Errors { get; set; }
}
public class ResultDto
{
    public ResultDto(string message, bool succeeded)
    {
        Message = message;
        this.Succeeded = succeeded;
    }
    public string Message { get; set; }
    public bool Succeeded { get; set; }
}



