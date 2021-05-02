namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTableConsultation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefConsultations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefPatientId = c.Int(nullable: false),
                        RefUserId = c.Int(nullable: false),
                        RefReceptionId = c.Int(),
                        Date = c.DateTime(nullable: false),
                        RefServiceId = c.Int(nullable: false),
                        RefDiagnosisId = c.Int(nullable: false),
                        Conclusion = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RefDiagnosis", t => t.RefDiagnosisId, cascadeDelete: false)
                .ForeignKey("dbo.RefPatients", t => t.RefPatientId, cascadeDelete: false)
                .ForeignKey("dbo.RefReceptions", t => t.RefReceptionId)
                .ForeignKey("dbo.RefServices", t => t.RefServiceId, cascadeDelete: false)
                .ForeignKey("dbo.RefUsers", t => t.RefUserId, cascadeDelete: false)
                .Index(t => t.RefPatientId)
                .Index(t => t.RefUserId)
                .Index(t => t.RefReceptionId)
                .Index(t => t.RefServiceId)
                .Index(t => t.RefDiagnosisId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefConsultations", "RefUserId", "dbo.RefUsers");
            DropForeignKey("dbo.RefConsultations", "RefServiceId", "dbo.RefServices");
            DropForeignKey("dbo.RefConsultations", "RefReceptionId", "dbo.RefReceptions");
            DropForeignKey("dbo.RefConsultations", "RefPatientId", "dbo.RefPatients");
            DropForeignKey("dbo.RefConsultations", "RefDiagnosisId", "dbo.RefDiagnosis");
            DropIndex("dbo.RefConsultations", new[] { "RefDiagnosisId" });
            DropIndex("dbo.RefConsultations", new[] { "RefServiceId" });
            DropIndex("dbo.RefConsultations", new[] { "RefReceptionId" });
            DropIndex("dbo.RefConsultations", new[] { "RefUserId" });
            DropIndex("dbo.RefConsultations", new[] { "RefPatientId" });
            DropTable("dbo.RefConsultations");
        }
    }
}
