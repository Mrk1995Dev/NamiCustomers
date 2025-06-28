namespace NamiCustomers.Application.Services.SevenSoftServices.Dtos;

public class SevenSoftSetting
{
    public string BaseUrl { get; init; }
    public BankCodeTypeOfSevenSoft BankCodeTypeOfSevenSoft { get; init; }
    public int Sandogh { get; init; }
    public Guid ElhaghiyehId { get; init; }
}

public record BankCodeTypeOfSevenSoft(int Ayandeh, int Gardeshgari, int Shahr, int Day);