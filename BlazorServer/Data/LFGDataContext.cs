// This is a class for our DB Context for use with entity framework
using Microsoft.EntityFrameworkCore;

namespace BlazorServer.Data
{
    public class LFGDataContext : DbContext //this is an entitry framework class inherit from
    {
        public LFGDataContext (DbContextOptions<LFGDataContext> options) //this is a constructor
            : base(options) //pass the above to the base class
        {

        }
    }
}
