using System;
using System.Transactions;
using Hangfire;
using Hangfire.MySql;

namespace NbSites.Web.Helpers
{
    public static partial class HangfireSetup
    {
        public static IGlobalConfiguration ConfigMySql(this IGlobalConfiguration configuration, string dbConn)
        {
            configuration.UseStorage(new MySqlStorage(
                dbConn, new MySqlStorageOptions
                {
                    TransactionIsolationLevel = IsolationLevel.ReadCommitted,
                    QueuePollInterval = TimeSpan.FromSeconds(15),
                    JobExpirationCheckInterval = TimeSpan.FromHours(1),
                    CountersAggregateInterval = TimeSpan.FromMinutes(5),
                    PrepareSchemaIfNecessary = true,
                    DashboardJobListLimit = 50000,
                    TransactionTimeout = TimeSpan.FromMinutes(1),
                    TablesPrefix = "Hangfire"
                }));
            return configuration;
        }
    }
}
