namespace NamiCustomers.Infrastucture.ExternalServices.Email.Dtos;

public class FrogetPasswordDto
{
    public string ToEmail { get; set; }
    public string FullName { get; set; }
    public string NewPass { get; set; }
    public DateTime Date { get; set; }
    public string browser { get; set; }
    public string IpRequest { get; set; }
}
