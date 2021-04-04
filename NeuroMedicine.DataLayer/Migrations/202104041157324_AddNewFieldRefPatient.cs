namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewFieldRefPatient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefPatients", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.RefPatients", "Mobile", c => c.String());
            AddColumn("dbo.RefPatients", "Mail", c => c.String());
            AddColumn("dbo.RefPatients", "Address", c => c.String());
            AddColumn("dbo.RefPatients", "Passport", c => c.String());
            AddColumn("dbo.RefPatients", "Snills", c => c.String());
            AddColumn("dbo.RefPatients", "Policy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefPatients", "Policy");
            DropColumn("dbo.RefPatients", "Snills");
            DropColumn("dbo.RefPatients", "Passport");
            DropColumn("dbo.RefPatients", "Address");
            DropColumn("dbo.RefPatients", "Mail");
            DropColumn("dbo.RefPatients", "Mobile");
            DropColumn("dbo.RefPatients", "Gender");
        }
    }
}
