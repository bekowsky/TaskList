namespace TaskList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDBend : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserProjects");
            AddPrimaryKey("dbo.UserProjects", new[] { "User_Id", "Project_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserProjects");
            AddPrimaryKey("dbo.UserProjects", new[] { "Project_Id", "User_Id" });
        }
    }
}
