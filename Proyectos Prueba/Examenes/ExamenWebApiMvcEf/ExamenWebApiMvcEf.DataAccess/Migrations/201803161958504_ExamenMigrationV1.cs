namespace ExamenWebApiMvcEf.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ExamenMigrationV1 : DbMigration
    {        
        public override void Up()
        {
            //CreateIndex("Security.UsUser", "UserName", unique: true, clustered: true, name: "IndexUserName");
        }
        
        public override void Down()
        {
            //DropIndex("Security.UsUser", "IndexUserName");
        }
    }
}
