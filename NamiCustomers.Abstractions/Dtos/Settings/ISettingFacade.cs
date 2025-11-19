using Microsoft.Extensions.Options;

namespace NamiCustomers.Abstractions.Dtos.Settings
{
    public interface ISettingFacade
    {
        public EndPointSetting EndPointSetting { get; }
        public JWTSettings JWTSettings { get; }

    }
    public class SettingFacade : ISettingFacade
    {
        public EndPointSetting EndPointSetting { get; }
        public JWTSettings JWTSettings { get; }
        public SettingFacade(IOptions<EndPointSetting> options, IOptions<JWTSettings> jwtSetting)
        {
            EndPointSetting = options.Value;
            JWTSettings = jwtSetting.Value;
        }


    }
}
