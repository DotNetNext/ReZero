using ReZero.DependencyInjection;
using System.Runtime.InteropServices;

namespace DependencyInjectionTest.Controllers
{
    public class Class1:IClass1, IScopeContract
    {
        public string? a { get; set; } = "a";
    }
    public class Class2 : IScopeContract
    {
        public string? a2 { get; set; } = "a2";
    }
    public interface IClass1
    {
    }
}
