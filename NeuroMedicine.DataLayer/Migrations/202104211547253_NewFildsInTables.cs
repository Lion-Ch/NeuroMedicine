namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFildsInTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefPatients", "PassportInfo", c => c.String());
            AddColumn("dbo.RefServices", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.RefServices", "Duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefServices", "Duration");
            DropColumn("dbo.RefServices", "Price");
            DropColumn("dbo.RefPatients", "PassportInfo");
        }
    }
}
