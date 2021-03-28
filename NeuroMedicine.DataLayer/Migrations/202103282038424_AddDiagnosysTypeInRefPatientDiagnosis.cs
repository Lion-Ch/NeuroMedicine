namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDiagnosysTypeInRefPatientDiagnosis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefPatientDiagnosis", "DiagnosysType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefPatientDiagnosis", "DiagnosysType");
        }
    }
}
