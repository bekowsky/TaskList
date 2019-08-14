namespace TaskList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserProjects");
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.Byte(nullable: false),
                        Online = c.DateTime(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        IsSender = c.Boolean(nullable: false),
                        FriendId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Friends", t => t.FriendId)
                .Index(t => t.FriendId);
            
            AddColumn("dbo.Projects", "Creator", c => c.String());
            AddColumn("dbo.Projects", "ReadPermission", c => c.String());
            AddColumn("dbo.Projects", "ChangePermission", c => c.String());
            AddColumn("dbo.Projects", "SettingsPermission", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String(maxLength: 12));
            AlterColumn("dbo.Users", "Password", c => c.String(maxLength: 12));
            AlterColumn("dbo.Users", "Phone", c => c.String(maxLength: 12));
            AddPrimaryKey("dbo.UserProjects", new[] { "Project_Id", "User_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friends", "UserId", "dbo.Users");
            DropForeignKey("dbo.Messages", "FriendId", "dbo.Friends");
            DropIndex("dbo.Messages", new[] { "FriendId" });
            DropIndex("dbo.Friends", new[] { "UserId" });
            DropPrimaryKey("dbo.UserProjects");
            AlterColumn("dbo.Users", "Phone", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
            DropColumn("dbo.Projects", "SettingsPermission");
            DropColumn("dbo.Projects", "ChangePermission");
            DropColumn("dbo.Projects", "ReadPermission");
            DropColumn("dbo.Projects", "Creator");
            DropTable("dbo.Messages");
            DropTable("dbo.Friends");
            AddPrimaryKey("dbo.UserProjects", new[] { "User_Id", "Project_Id" });
        }
    }
}
