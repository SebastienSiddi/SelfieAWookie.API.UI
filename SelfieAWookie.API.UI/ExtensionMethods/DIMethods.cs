using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Repositories;

namespace SelfieAWookie.API.UI.ExtensionMethods
{
    public static class DIMethods
    {
        #region Public methods
        /// <summary>
        /// Prepare customs dependency injections
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddInjections(this IServiceCollection services) 
        {
            services.AddScoped<ISelfieRepository, DefaultSelfieRepository>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            return services;
        }
        #endregion
    }
}
