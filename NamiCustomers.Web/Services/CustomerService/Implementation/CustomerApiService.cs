using NamiCustomers.Web.Models.Settings;
using NamiCustomers.Web.Services.CustomerService.Dto;

namespace NamiCustomers.Web.Services.CustomerService.Implementation
{

    public class CustomerService(HttpClient http, ISettingFacade settingFacade)
    {
        public async Task<List<CustomerListDto>> GetAllAsync()
        {
            //https://localhost:7061/v1/Customer/customerList
            //https://localhost:7061/customer/customerList
            var result = await http.GetFromJsonAsync<List<CustomerListDto>>($"{settingFacade.EndPointSetting.ISQIAPI.URL}/customer/customerList");
            return result;
        }

        public async Task<List<CityDto>> GetAllCitiesAsync()
        {
            var result = await http.GetFromJsonAsync<List<CityDto>>($"{settingFacade.EndPointSetting.ISQIAPI.URL}/City/list");
            return result;
        }

        public async Task<ResultDto<CustomerInfoDetailsDto>> GetByIdAsync(int id)
        {
            return await http.GetFromJsonAsync<ResultDto<CustomerInfoDetailsDto>>($"{settingFacade.EndPointSetting.ISQIAPI.URL}/customer/info?id={id}");
        }

        public async Task<ResultDto> CreateAsync(AddCustomerInfoDto customer)
        {
            //https://localhost:7061/api/v1/Customer/addCustomer
            var respone = await http.PostAsJsonAsync($"{settingFacade.EndPointSetting.ISQIAPI.URL}/customer/addCustomer", customer);
            if (respone.IsSuccessStatusCode)
            {
                return new ResultDto(
                    "مشتری جدید با موفقیت ثبت شد.",
                    true);
            }

            return new ResultDto(
                "خطا در ثبت مشتری جدید",
                false);
        }

        public async Task<ResultDto> UpdateAsync(UpdateCustomerInfoDto updateCustomer)
        {
            var response = await http.PutAsJsonAsync($"{settingFacade.EndPointSetting.ISQIAPI.URL}/customer/edit", updateCustomer);
            if (response.IsSuccessStatusCode)
            {
                return new ResultDto(
                    "مشتری با موفقیت ویرایش شد.",
                    true);
            }

            return new ResultDto(
                "خطا در ویرایش مشتری ",
                false);
        }

        public async Task<ResultDto> DeleteAsync(int id)
        {
            var response = await http.DeleteAsync($"{settingFacade.EndPointSetting.ISQIAPI.URL}/customer/remove?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return new ResultDto(
                    "مشتری با موفقیت حذف شد.",
                    true);
            }

            return new ResultDto(
                "خطا در حذف مشتری ",
                false);
        }
    }
}