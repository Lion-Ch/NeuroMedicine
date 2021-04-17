namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewServiceBloodTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefBloodTests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefPatientId = c.Int(nullable: false),
                        RefUserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ESR = c.Double(nullable: false),
                        HB = c.Double(nullable: false),
                        Leukocytes = c.Double(nullable: false),
                        Erythrocytes = c.Double(nullable: false),
                        ErythrocyteIndex = c.Double(nullable: false),
                        ChangeErythrocytes = c.Double(nullable: false),
                        Coagulability = c.Double(nullable: false),
                        Platelets = c.Double(nullable: false),
                        ProthrombinIndex = c.Double(nullable: false),
                        B = c.Double(nullable: false),
                        YU = c.Double(nullable: false),
                        P = c.Double(nullable: false),
                        FROM = c.Double(nullable: false),
                        L = c.Double(nullable: false),
                        M = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RefPatients", t => t.RefPatientId, cascadeDelete: true)
                .ForeignKey("dbo.RefUsers", t => t.RefUserId, cascadeDelete: true)
                .Index(t => t.RefPatientId)
                .Index(t => t.RefUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefBloodTests", "RefUserId", "dbo.RefUsers");
            DropForeignKey("dbo.RefBloodTests", "RefPatientId", "dbo.RefPatients");
            DropIndex("dbo.RefBloodTests", new[] { "RefUserId" });
            DropIndex("dbo.RefBloodTests", new[] { "RefPatientId" });
            DropTable("dbo.RefBloodTests");
        }
    }
}
