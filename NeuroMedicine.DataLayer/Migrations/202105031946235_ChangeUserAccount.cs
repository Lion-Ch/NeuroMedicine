namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserAccount : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RefUserAccounts", "Login", c => c.String());
            AlterColumn("dbo.RefUserAccounts", "Password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RefUserAccounts", "Password", c => c.Int(nullable: false));
            AlterColumn("dbo.RefUserAccounts", "Login", c => c.Int(nullable: false));
        }
    }
}
