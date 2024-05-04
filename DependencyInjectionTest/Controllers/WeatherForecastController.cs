using Microsoft.AspNetCore.Mvc;
using ReZero.DependencyInjection;

namespace DependencyInjectionTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [PropertyInjection]
        public Class2? Class2 { get; set; }

        [HttpGet(Name = "GetWeatherForecast")]
        public object Get()
        {
            var class1=DependencyResolver.GetHttpContextService<Class1>();
            var class2 = DependencyResolver.GetHttpContextService<Class1>();
            return  class1.GetHashCode()+"_"+class2.GetHashCode()  ;
        }
    }
}
