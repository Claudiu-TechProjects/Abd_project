using System.Data.Entity;
using ABD_Project.Classes;

namespace ABD_Project.Database
{
    public class DBContext : DbContext
    {
        public DBContext() : base("name=bookingConnectionString") { }

        public DbSet<Users> Users { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
