using Microsoft.AspNetCore.Mvc;

namespace ReZeroWeb.Controllers
{
    /// <summary>
    /// ԭ���ӿ�
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