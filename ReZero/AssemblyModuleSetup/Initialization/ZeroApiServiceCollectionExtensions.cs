using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ReZero.DependencyInjection;
using ReZero.SuperAPI;
using System;
using System.Linq;
using System.Text;
namespace ReZero 
{

    public static partial class ReZeroServiceCollectionExtensions
    {

        /// <summary>
        /// Adds ReZero services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="options">The <see cref="ReZeroOptions"/> to configure the services.</param>
        /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddReZeroServices(this IServiceCollection services, ReZeroOptions options)
        {
            ServiceLocator.Services = services;
            SuperAPIModule.Init(services, options);
            AddDependencyInjection(options, options.SuperApiOptions);
            DependencyInjectionModule.Init(services, options); 
            JwtInit(services, options); 
            return services;
        }

        private static void JwtInit(IServiceCollection services, ReZeroOptions options)
        {
            var key = options.SuperApiOptions.InterfaceOptions?.Jwt?.Secret + "";
            if (string.IsNullOrEmpty(key)|| options.SuperApiOptions.InterfaceOptions?.Jwt?.Enable!=true) 
            {
                return;
            }
            var keyBytes = Encoding.ASCII.GetBytes(key);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
                };
            });
        }

        public static IServiceCollection AddReZeroServices(this IServiceCollection services, Action<SuperAPIOptions> superAPIOptions)
        {
            var options = new ReZeroOptions();
            ServiceLocator.Services = services; 
            superAPIOptions(options.SuperApiOptions); 
            return services.AddReZeroServices(options);
        }
        internal static void AddDependencyInjection(ReZeroOptions options, SuperAPIOptions superAPIOptions)
        {
            if (options.DependencyInjectionOptions?.Assemblies?.Any()!=true)
            {
                if (options.DependencyInjectionOptions == null)
                {
                    options.DependencyInjectionOptions = new DependencyInjection.DependencyInjectionOptions();
                }
                options.DependencyInjectionOptions!.Assemblies = superAPIOptions?.DependencyInjectionOptions?.Assemblies;
            }
        }
    }
}