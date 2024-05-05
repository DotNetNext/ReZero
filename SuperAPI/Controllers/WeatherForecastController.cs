using Microsoft.AspNetCore.Mvc;

namespace ReZeroWeb.Controllers
{
    /// <summary>
    /// 原生接口
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    { 
        [HttpGet(Name = "GetWeatherForecast")]
        public string Get()
        {
             return "Hello word" ;
        }
    }
}