// This is a class for our DB Context for use with entity framework
using Microsoft.EntityFrameworkCore;

namespace BlazorServer.Data
{
    public class ComputerDataContext : DbContext //this is an entitry framework class inherit from
    {
        public ComputerDataContext(DbContextOptions<ComputerDataContext> options) //this is a constructor
            : base(options) //pass the above to the base class
        {

        }
        public DbSet<BlazorServer.Data.Computer> Computer { get; set; } = default!;
    }
}