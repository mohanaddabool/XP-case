using Microsoft.EntityFrameworkCore;
using XPAssignment.DbModels;

namespace XPAssignment.Context
{
    public class XPAssignmentDbContext : DbContext
    {
        public XPAssignmentDbContext(DbContextOptions<XPAssignmentDbContext> options): base(options)
        {
            
        }

        public DbSet<Show> Shows { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
