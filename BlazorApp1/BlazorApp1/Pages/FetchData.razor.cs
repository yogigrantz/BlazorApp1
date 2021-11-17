using BlazorApp1.Data;

namespace BlazorApp1.Pages
{
    public partial class FetchData
    {
        public WeatherForecast[]? forecasts;

        public string ISAuthenticated { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            forecasts = await ForecastService.GetForecastAsync(DateTime.Now);

            string username = await authentication.IsAuthorized(httpContextAccessor, "Admin");

            if (username != null)
            {
                ISAuthenticated = $"User {username} is authenticated";
            }

        }
    
    }
}
