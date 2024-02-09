namespace SelfieAWookie.API.UI.ExtensionMethods
{
    /// <summary>
    /// About security (CORS, JWT, etc.)
    /// </summary>
    public static class SecurityMethods
    {
        #region Constants
        public const string DEFAULT_POLICY = "DEFAULT_POLICY";        
        #endregion

        #region Public methods
        /// <summary>
        /// Add Cors and JWT configuration
        /// </summary>
        /// <param name="services"></param>
        public static void AddCustomSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DEFAULT_POLICY", builder =>
                {
                    builder.WithOrigins(configuration["Cors:Origin"])
                           .AllowAnyHeader()
                           .AllowAnyMethod();                           
                });               
            });            
        }
        #endregion
    }
}
