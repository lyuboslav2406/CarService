namespace CarService.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Net;

    using CarService.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Nancy.Json;
    using WeatherForecast.Models;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult Weather()
        {
            return this.View();
        }

        [HttpGet]
        public string WeatherDetail(string city)
        {
            // Assign API KEY which received from OPENWEATHERMAP.ORG
            string appId = "8113fcc5a7494b0518bd91ef3acc074f";

            // API path with CITY parameter and other parameters.
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", city, appId);

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                // Converting to OBJECT from JSON string.
                RootObject weatherInfo = new JavaScriptSerializer().Deserialize<RootObject>(json);

                // Special VIEWMODEL design to send only required fields not all fields which received from
                // www.openweathermap.org api
                ResultViewModel rslt = new ResultViewModel();

                rslt.Country = weatherInfo.sys.country;
                rslt.City = weatherInfo.name;
                rslt.Lat = Convert.ToString(weatherInfo.coord.lat);
                rslt.Lon = Convert.ToString(weatherInfo.coord.lon);
                rslt.Description = weatherInfo.weather[0].description;
                rslt.Humidity = Convert.ToString(weatherInfo.main.humidity);
                rslt.Temp = Convert.ToString(weatherInfo.main.temp);
                rslt.TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like);
                rslt.TempMax = Convert.ToString(weatherInfo.main.temp_max);
                rslt.TempMin = Convert.ToString(weatherInfo.main.temp_min);
                rslt.WeatherIcon = weatherInfo.weather[0].icon;

                // Converting OBJECT to JSON String
                var jsonstring = new JavaScriptSerializer().Serialize(rslt);

                // Return JSON string.
                return jsonstring;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
