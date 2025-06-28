namespace NamiCustomers.Application.Services.Facades.Dtos;

public class HostedServiceSetting
{
    public HostedServiceItem SevenSoft { get; set; }
    public HostedServiceItem IranFava { get; set; }
    
}

public record HostedServiceItem
{
    public bool IsEnable { get; init; }
    public long TimeSpanMinutely { get; init; }
}


 