namespace ExamenWebApiMvcEf.DataAccess.Migrations
{
    using Models.Models;

    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ExamenContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ExamenContext context)
        {
            context.UsUserType.AddOrUpdate(
                new UsUserType
                {
                    IdUserType = 1,
                    UserType = "Administrador General",
                    Description = "Usuario con control total de sistema"
                });
        }
    }
}
