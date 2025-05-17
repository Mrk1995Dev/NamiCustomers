using Microsoft.Extensions.Options;

namespace NamiCustomers.Web.Models.Settings
{
    public interface ISettingFacade
    {
        public EndPointSetting EndPointSetting { get; }
        
    }
    public class SettingFacade:ISettingFacade
    { 
        public EndPointSetting EndPointSetting { get; }
        public SettingFacade(IOptions<EndPointSetting> options)
        {
            EndPointSetting = options.Value;
        }

       
    }
}
