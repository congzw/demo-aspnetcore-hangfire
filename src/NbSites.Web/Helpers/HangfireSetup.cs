using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NbSites.Web.Helpers
{
    public static partial class HangfireSetup
    {
        public static void AddConfigHangfire(this IServiceCollection services, IConfiguration config)
        {
            var dbConn = config.GetConnectionString("HangfireConnection");
            
            //ensure the hangfireDb exist
            using (var hangfireDbContext = new HangfireDbContext(dbConn))
            {
                hangfireDbContext.Database.EnsureCreated();
            }
            
            services.AddHangfire(gc => gc
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseColouredConsoleLogProvider()
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                //.ConfigSqlServer(dbConn));
                .ConfigMySql(dbConn));
        }
    }
}
