using System;
using TP_Blazor.Data;

namespace TP_Blazor.Pages
{
    public partial class FetchData
    {
        public FetchData()
        {
        }
        private WeatherForecast[]? forecasts;

        protected override async Task OnInitializedAsync()
        {
            forecasts = await ForecastService.GetForecastAsync(DateOnly.FromDateTime(DateTime.Now));
        }
    }
}

