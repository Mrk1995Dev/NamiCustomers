
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NamiCustomers.Application.Services.Appointments;
using NamiCustomers.Application.Services.Dealers;
using NamiCustomers.Application.Services.Subscribers;
using NamiCustomers.Application.Services.Vehicles;
using NamiCustomers.Infrastucture.ExternalServices.Email;


namespace NamiCustomers.Application
{
    public static class ServicesRegisteration
    {
        public static IServiceCollection ConfigurationApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ISAMPLEService, SAMPLEService>();
            services.AddScoped<ISubscriberService, SubscriberService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IDealerService, DealerService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IMailService, MailService>();

            #region SmsService

            services.AddScoped<JiringSmsService>();
            services.AddScoped<KaraShobSmsService>();
            services.AddScoped<ISmsService>(serviceProvider =>
            {
                var smsProviders = configuration.ReadSmsSettings();
                var provider = smsProviders.Single(c => c.IsDefault);
                return provider.ProviderName switch
                {
                    "Jiring" => serviceProvider.GetRequiredService<JiringSmsService>(),
                    "KaraShob" => serviceProvider.GetRequiredService<KaraShobSmsService>(),
                    _ => throw new KeyNotFoundException(),
                };
            });
            #endregion

            return services;
        }

        /// <summary>
        ///  ToDo moradi : فعلا برای .net9  پکیج ConfiiguratioBinder  نیامده است 14031101
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static List<SmsSetting> ReadSmsSettings(this IConfiguration configuration)
        {
            IConfigurationSection smsSection = configuration.GetSection("SmsSettings");
            IEnumerable<IConfigurationSection> usersArray = smsSection.GetChildren();

            return usersArray.Select(configSection =>
                new SmsSetting
                {
                    ProviderName = configSection["ProviderName"]!.ToString(),
                    IsDefault = bool.Parse(configSection["IsDefault"]!.ToString())
                }).ToList();
        }
    }
}