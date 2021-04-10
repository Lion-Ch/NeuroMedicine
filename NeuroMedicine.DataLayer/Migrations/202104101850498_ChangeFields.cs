namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefPatientDiagnosis", "RefDiagnosisId", c => c.Int(nullable: false));
            CreateIndex("dbo.RefPatientDiagnosis", "RefDiagnosisId");
            AddForeignKey("dbo.RefPatientDiagnosis", "RefDiagnosisId", "dbo.RefDiagnosis", "Id", cascadeDelete: false);
            DropColumn("dbo.RefPatientDiagnosis", "DiagnosysType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RefPatientDiagnosis", "DiagnosysType", c => c.Int(nullable: false));
            DropForeignKey("dbo.RefPatientDiagnosis", "RefDiagnosisId", "dbo.RefDiagnosis");
            DropIndex("dbo.RefPatientDiagnosis", new[] { "RefDiagnosisId" });
            DropColumn("dbo.RefPatientDiagnosis", "RefDiagnosisId");
        }
    }
}
