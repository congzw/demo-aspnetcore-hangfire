using System;
using Hangfire;
using Hangfire.SqlServer;

namespace NbSites.Web.Helpers
{
    public static partial class HangfireSetup
    {
        public static IGlobalConfiguration ConfigSqlServer(this IGlobalConfiguration configuration, string dbConn)
        {
            configuration
                .UseSqlServerStorage(dbConn, new SqlServerStorageOptions
                 {
                     CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                     SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                     QueuePollInterval = TimeSpan.Zero,
                     UseRecommendedIsolationLevel = true,
                     DisableGlobalLocks = true
                 });

            return configuration;
        }
    }
}
