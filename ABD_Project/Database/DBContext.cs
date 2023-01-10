using System.Data.Entity;


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
