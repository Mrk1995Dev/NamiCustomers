using Microsoft.JSInterop;

namespace NamiCustomers.Web.Services.LocalStorage
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime jSRuntime;

        public LocalStorageService(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime;
        }

        public async Task<string?> GetItemAsync(string key)
        {
            return await jSRuntime.InvokeAsync<string>("localStorage.getItem", key);
        }

        public async Task RemoveItemAsync(string key)
        {
            await jSRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public async Task SetItemAsync(string key, string value)
        {
            await jSRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
        }
    }
}
