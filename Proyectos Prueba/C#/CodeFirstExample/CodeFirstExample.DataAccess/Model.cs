namespace CodeFirstExample.DataAccess
{
    using Models;
    using System.Data.Entity;

    public class Model
    {
        
        public class SchoolContext : DbContext
        {
            public SchoolContext() : base("name=SchoolDBConnectionString")
            {
                //Database.SetInitializer(new CreateDatabaseIfNotExists<SchoolContext>());
                Database.SetInitializer(new DropCreateDatabaseAlways<SchoolContext>());
            }

            public DbSet<Student> Students { get; set; }
            public DbSet<Standard> Standards { get; set; }

        }

        public class SchoolDbInitializer : DropCreateDatabaseAlways<SchoolContext>
        {
            protected override void Seed(SchoolContext context)
            {
                base.Seed(context);
            }
        }
    }
}
