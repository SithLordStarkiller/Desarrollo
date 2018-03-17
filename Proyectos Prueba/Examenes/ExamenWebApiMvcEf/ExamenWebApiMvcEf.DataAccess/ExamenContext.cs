namespace ExamenWebApiMvcEf.DataAccess
{
    using Models.Models;

    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ExamenContext : DbContext
    {
        public ExamenContext() : base("name=DbConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ExamenContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UsUserType>().Property(c => c.IdUserType).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UsUser> UsUser { get; set; }
        public DbSet<UsUserType> UsUserType { get; set; }
    }
}
