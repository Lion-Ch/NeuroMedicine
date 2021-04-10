namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsUseNeuralNetwork = c.Boolean(nullable: false),
                        PathToNeuralNetwork = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.RefPatientDiagnosis", "RefServiceId", c => c.Int(nullable: false));
            AddColumn("dbo.RefReceptions", "RefServiceId", c => c.Int(nullable: false));
            CreateIndex("dbo.RefPatientDiagnosis", "RefServiceId");
            CreateIndex("dbo.RefReceptions", "RefServiceId");
            AddForeignKey("dbo.RefReceptions", "RefServiceId", "dbo.RefServices", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RefPatientDiagnosis", "RefServiceId", "dbo.RefServices", "Id", cascadeDelete: true);
            DropColumn("dbo.RefPatientDiagnosis", "DiagnosticType");
            DropColumn("dbo.RefReceptions", "DiagnosticType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RefReceptions", "DiagnosticType", c => c.Int(nullable: false));
            AddColumn("dbo.RefPatientDiagnosis", "DiagnosticType", c => c.Int(nullable: false));
            DropForeignKey("dbo.RefPatientDiagnosis", "RefServiceId", "dbo.RefServices");
            DropForeignKey("dbo.RefReceptions", "RefServiceId", "dbo.RefServices");
            DropIndex("dbo.RefReceptions", new[] { "RefServiceId" });
            DropIndex("dbo.RefPatientDiagnosis", new[] { "RefServiceId" });
            DropColumn("dbo.RefReceptions", "RefServiceId");
            DropColumn("dbo.RefPatientDiagnosis", "RefServiceId");
            DropTable("dbo.RefServices");
        }
    }
}
