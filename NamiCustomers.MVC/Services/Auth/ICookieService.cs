namespace NamiCustomers.MVC.Services.Auth
{
    public interface ICookieService
    {
        void SetCookie(string key, string value, int? expireTime = null);
        string GetCookie(string key);
        void RemoveCookie(string key);
    }

    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetCookie(string key, string value, int? expireTime = null)
        {
            var options = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };

            if (expireTime.HasValue)
                options.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                options.Expires = DateTime.Now.AddHours(2);

            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, options);
        }

        public string GetCookie(string key)
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[key];
        }

        public void RemoveCookie(string key)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
        }
    }
}
