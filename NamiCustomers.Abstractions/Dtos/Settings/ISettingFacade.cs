using Microsoft.Extensions.Options;

namespace NamiCustomers.Abstractions.Dtos.Settings
{
    public interface ISettingFacade
    {
        public EndPointSetting EndPointSetting { get; }

    }
    public class SettingFacade : ISettingFacade
    {
        public EndPointSetting EndPointSetting { get; }
        public SettingFacade(IOptions<EndPointSetting> options)
        {
            EndPointSetting = options.Value;
        }


    }
}
