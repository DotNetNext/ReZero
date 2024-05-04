using Microsoft.AspNetCore.Mvc;
using ReZero.DependencyInjection;

namespace DependencyInjectionTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
      

        public WeatherForecastController( )
        {
        
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public object Get()
        {
            var class1=DependencyResolver.GetService<Class1>();
            var class2 = DependencyResolver.GetService<IClass1>();
            return 1;
        }
    }
}
