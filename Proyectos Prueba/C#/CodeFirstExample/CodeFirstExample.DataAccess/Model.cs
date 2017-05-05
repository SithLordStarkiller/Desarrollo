namespace CodeFirstExample.DataAccess
{
    using Models;
    using System.Data.Entity;

    public class Model
    {
        
        public class SchoolContext : DbContext
        {
            public SchoolContext() : base()
            {

            }

            public DbSet<Student> Students { get; set; }
            public DbSet<Standard> Standards { get; set; }

        }
    }
}
