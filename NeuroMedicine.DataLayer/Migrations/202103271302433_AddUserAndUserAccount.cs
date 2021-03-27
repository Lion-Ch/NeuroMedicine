namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserAndUserAccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefUserAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RefUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.RefUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        UserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefUserAccounts", "Id", "dbo.RefUsers");
            DropIndex("dbo.RefUserAccounts", new[] { "Id" });
            DropTable("dbo.RefUsers");
            DropTable("dbo.RefUserAccounts");
        }
    }
}
