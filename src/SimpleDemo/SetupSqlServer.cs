using System;
using Hangfire;
using Hangfire.SqlServer;

namespace SimpleDemo
{
    public class SetupSqlServer
    {
        public static void Config()
        {
            //var dbConn = "Database=HangfireSampleDb; Integrated Security=True;";
            var dbConn = "Server=(localdb)\\mssqllocaldb; Database=HangfireSampleDb; Trusted_Connection=True";
            
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseColouredConsoleLogProvider()
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(dbConn, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                });
        }
    }
}