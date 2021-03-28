namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRefPatientDiagnosis : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefPatientDiagnosis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefPatientId = c.Int(nullable: false),
                        RefUserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        DiagnosticType = c.Int(nullable: false),
                        Conclusion = c.String(),
                        ResultNeuralNetwork = c.Double(nullable: false),
                        PhotoUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RefPatients", t => t.RefPatientId, cascadeDelete: true)
                .ForeignKey("dbo.RefUsers", t => t.RefUserId, cascadeDelete: true)
                .Index(t => t.RefPatientId)
                .Index(t => t.RefUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefPatientDiagnosis", "RefUserId", "dbo.RefUsers");
            DropForeignKey("dbo.RefPatientDiagnosis", "RefPatientId", "dbo.RefPatients");
            DropIndex("dbo.RefPatientDiagnosis", new[] { "RefUserId" });
            DropIndex("dbo.RefPatientDiagnosis", new[] { "RefPatientId" });
            DropTable("dbo.RefPatientDiagnosis");
        }
    }
}
