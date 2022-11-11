using CallLogger.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CallLogger.Data
{
    public class CallLogDbContext : DbContext
    {
        public CallLogDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<CallLog> CallLogger{ get; set; }
        

    }
}
