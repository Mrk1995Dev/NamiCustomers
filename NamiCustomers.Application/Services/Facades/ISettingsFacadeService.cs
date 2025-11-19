using Microsoft.Extensions.Options;
using NamiCustomers.Abstractions.Dtos.Settings;
using NamiCustomers.Application.Services.Facades.Dtos;
using NamiCustomers.Infrastucture.ExternalServices.Email.Dtos;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

namespace NamiCustomers.Application.Services.Facades;

public interface ISettingsFacadeService
{
    public MailSettings MailSettings { get; init; }
    public SmsSetting SmsSetting { get; init; }
    public SevenSoftSetting SevenSoftSetting { get; init; }
    public CompanySetting CompanySetting { get; init; }
    public HostedServiceSetting HotedServiceSetting { get; init; }
    public JWTSettings JWTSetting { get; init; }
}

public class SettingsFacadeService : ISettingsFacadeService
{

    public JWTSettings JWTSetting { get; init; }

    public MailSettings MailSettings { get; init; }
    public SmsSetting SmsSetting { get; init; }
    public SevenSoftSetting SevenSoftSetting { get; init; }
    public CompanySetting CompanySetting { get; init; }
    public HostedServiceSetting HotedServiceSetting { get; init; }


    public SettingsFacadeService(
                                IOptions<List<SmsSetting>> smsSetting,
                                IOptions<SevenSoftSetting> sevenSoftSetting,
                                IOptions<MailSettings> mailSettings,
                                IOptions<CompanySetting> companySetting,
                                IOptions<HostedServiceSetting> hotedServiceSetting,
                                 IOptions<JWTSettings> jwtSetting
        )
    {
        MailSettings = mailSettings.Value;
        SmsSetting = smsSetting.Value.Where(c => c.IsDefault).Single();
        SevenSoftSetting = sevenSoftSetting.Value;
        CompanySetting = companySetting.Value;
        HotedServiceSetting = hotedServiceSetting.Value;
        JWTSetting = jwtSetting.Value;
}



}
