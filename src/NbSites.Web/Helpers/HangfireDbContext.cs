using Microsoft.EntityFrameworkCore;

namespace NbSites.Web.Helpers
{
    public class HangfireDbContext : DbContext
    {
        private readonly string _dbConn;

        public HangfireDbContext(string dbConn)
        {
            _dbConn = dbConn;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_dbConn);
        }
    }
}
