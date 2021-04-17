namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFldInReception : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefReceptions", "RefUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.RefReceptions", "RefUserId");
            AddForeignKey("dbo.RefReceptions", "RefUserId", "dbo.RefUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefReceptions", "RefUserId", "dbo.RefUsers");
            DropIndex("dbo.RefReceptions", new[] { "RefUserId" });
            DropColumn("dbo.RefReceptions", "RefUserId");
        }
    }
}
