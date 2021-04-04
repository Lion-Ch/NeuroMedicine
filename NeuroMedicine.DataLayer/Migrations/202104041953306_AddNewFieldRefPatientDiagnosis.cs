namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewFieldRefPatientDiagnosis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefPatientDiagnosis", "RefReceptionId", c => c.Int(nullable: true));
            CreateIndex("dbo.RefPatientDiagnosis", "RefReceptionId");
            AddForeignKey("dbo.RefPatientDiagnosis", "RefReceptionId", "dbo.RefReceptions", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefPatientDiagnosis", "RefReceptionId", "dbo.RefReceptions");
            DropIndex("dbo.RefPatientDiagnosis", new[] { "RefReceptionId" });
            DropColumn("dbo.RefPatientDiagnosis", "RefReceptionId");
        }
    }
}
