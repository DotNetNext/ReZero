using Microsoft.Extensions.DependencyInjection;
using System;

namespace ReZero
{
 
    /// <summary>
    /// 应用程序的主要类，用于管理依赖注入和服务定位。
    /// </summary>
    public class ApplicationServiceProvider
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数，接受一个 <see cref="IServiceProvider"/> 实例。
        /// </summary>
        /// <param name="serviceProvider">依赖注入容器。</param>
        public ApplicationServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <summary>
        /// 获取指定类型的服务实例。
        /// </summary>
        /// <typeparam name="T">要获取的服务类型。</typeparam>
        /// <returns>指定类型的服务实例。</returns>
        public T GetService<T>()
        {
         
#pragma warning disable CS8603 
            return _serviceProvider.GetService<T>();
#pragma warning restore CS8603  
        }
    }
}