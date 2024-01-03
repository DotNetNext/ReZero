using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ReZero.SuperAPI
{

    /// <summary>
    /// The main class of the application, used for managing dependency injection and service location.
    /// </summary>
    public class ApplicationServiceProvider
    {
        private readonly IApplicationBuilder _app;

        /// <summary>
        /// Constructor that accepts an <see cref="IServiceProvider"/> instance.
        /// </summary>
        /// <param name="serviceProvider">The dependency injection container.</param>
        public ApplicationServiceProvider(IApplicationBuilder serviceProvider)
        {
            _app = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <summary>
        /// Gets an instance of the specified service type.
        /// </summary>
        /// <typeparam name="T">The type of service to retrieve.</typeparam>
        /// <returns>An instance of the specified service type.</returns>
        public T GetService<T>() where T : class
        {
            // Get the IOC container (service provider)
            var serviceProvider = _app.ApplicationServices;

            // Perform the operation using the IOC container
            var myService = serviceProvider!.GetRequiredService<T>();
            return myService;
        }
    }
}