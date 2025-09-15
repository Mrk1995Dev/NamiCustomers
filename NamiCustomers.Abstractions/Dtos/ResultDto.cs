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

    public bool Succeeded { get; private set; }
    public string? Message { get; private set; }
    public T? Data { get; private set; }
    public List<string> Errors { get; private set; }
}
public class ResultDto
{
    public static ResultDto Success(string message)
    {
        return new ResultDto(message, true);
    }
    public static ResultDto Failure(string message)
    {
        return new ResultDto(message, false);
    }
    public ResultDto()
    {
            
    }
    private ResultDto(string message, bool succeeded)
    {
        Message = message;
        this.Succeeded = succeeded;
    }
    public string Message { get; private set; }
    public bool Succeeded { get; private set; }
}



