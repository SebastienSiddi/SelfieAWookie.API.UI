using SelfieAWookies.Core.Selfies.Infrastructures.Configurations;

namespace SelfieAWookie.API.UI.ExtensionMethods
{
    /// <summary>
    /// Custom options from configuration (json)
    /// </summary>
    public static class OptionMethods
    {
        #region Public methods
        /// <summary>
        /// Add custom options from configuration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SecurityOption>(configuration.GetSection("Jwt"));
        }
        #endregion
    }
}
