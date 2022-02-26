using LoginDetails.OpenWeatherMapModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDetails.Repositories
{
    public interface IForecastRepository
    {
        WeatherResponse GetForecast(string city);
    }
}
