namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableRefReception : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefReceptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefPatientId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DiagnosticType = c.Int(nullable: false),
                        DateRecording = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RefPatients", t => t.RefPatientId, cascadeDelete: true)
                .Index(t => t.RefPatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefReceptions", "RefPatientId", "dbo.RefPatients");
            DropIndex("dbo.RefReceptions", new[] { "RefPatientId" });
            DropTable("dbo.RefReceptions");
        }
    }
}
