namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTableRefUrineTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefUrineTests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefPatientId = c.Int(nullable: false),
                        RefUserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Quantity = c.String(),
                        Color = c.String(),
                        Transparency = c.String(),
                        SpecificGravity = c.Double(nullable: false),
                        Reaction = c.String(),
                        Protein = c.Double(nullable: false),
                        Sugar = c.Double(nullable: false),
                        Cylinders = c.Double(nullable: false),
                        RenalEpithelium = c.Double(nullable: false),
                        Erythrocytes = c.Double(nullable: false),
                        Leukocytes = c.Double(nullable: false),
                        Epithelium = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RefPatients", t => t.RefPatientId, cascadeDelete: true)
                .ForeignKey("dbo.RefUsers", t => t.RefUserId, cascadeDelete: true)
                .Index(t => t.RefPatientId)
                .Index(t => t.RefUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefUrineTests", "RefUserId", "dbo.RefUsers");
            DropForeignKey("dbo.RefUrineTests", "RefPatientId", "dbo.RefPatients");
            DropIndex("dbo.RefUrineTests", new[] { "RefUserId" });
            DropIndex("dbo.RefUrineTests", new[] { "RefPatientId" });
            DropTable("dbo.RefUrineTests");
        }
    }
}
