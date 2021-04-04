namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableIdReception : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RefPatientDiagnosis", "RefReceptionId", "dbo.RefReceptions");
            DropIndex("dbo.RefPatientDiagnosis", new[] { "RefReceptionId" });
            AlterColumn("dbo.RefPatientDiagnosis", "RefReceptionId", c => c.Int());
            CreateIndex("dbo.RefPatientDiagnosis", "RefReceptionId");
            AddForeignKey("dbo.RefPatientDiagnosis", "RefReceptionId", "dbo.RefReceptions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefPatientDiagnosis", "RefReceptionId", "dbo.RefReceptions");
            DropIndex("dbo.RefPatientDiagnosis", new[] { "RefReceptionId" });
            AlterColumn("dbo.RefPatientDiagnosis", "RefReceptionId", c => c.Int(nullable: false));
            CreateIndex("dbo.RefPatientDiagnosis", "RefReceptionId");
            AddForeignKey("dbo.RefPatientDiagnosis", "RefReceptionId", "dbo.RefReceptions", "Id", cascadeDelete: true);
        }
    }
}
