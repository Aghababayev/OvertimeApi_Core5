using Microsoft.EntityFrameworkCore;

namespace OvertimeApi.DataAceess
{
    public class Context : DbContext
    {
        
        public Context( DbContextOptions<Context> options) : base(options)
        {
        }

        public  DbSet<Overtime> Overtimes { get; set; }   
        public  DbSet<User> Users { get; set; }    
    }
}
