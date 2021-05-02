namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMeasurment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefMeasurments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefPatientId = c.Int(nullable: false),
                        RefUserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Height = c.Single(nullable: false),
                        Weight = c.Single(nullable: false),
                        Pulse = c.Int(nullable: false),
                        PressureSystolic = c.Int(nullable: false),
                        PressureDiastolic = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RefPatients", t => t.RefPatientId, cascadeDelete: true)
                .ForeignKey("dbo.RefUsers", t => t.RefUserId, cascadeDelete: true)
                .Index(t => t.RefPatientId)
                .Index(t => t.RefUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefMeasurments", "RefUserId", "dbo.RefUsers");
            DropForeignKey("dbo.RefMeasurments", "RefPatientId", "dbo.RefPatients");
            DropIndex("dbo.RefMeasurments", new[] { "RefUserId" });
            DropIndex("dbo.RefMeasurments", new[] { "RefPatientId" });
            DropTable("dbo.RefMeasurments");
        }
    }
}
