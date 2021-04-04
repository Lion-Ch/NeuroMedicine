namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLengthFullNameRefPatient : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RefPatients", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RefPatients", "FullName", c => c.String(maxLength: 60));
        }
    }
}
