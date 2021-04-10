namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTables1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefDiagnosis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RefServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RefServices", t => t.RefServiceId, cascadeDelete: true)
                .Index(t => t.RefServiceId);
            
            CreateTable(
                "dbo.RefDoctorSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefUserId = c.Int(nullable: false),
                        NumDay = c.Int(nullable: false),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                        NumPatients = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RefUsers", t => t.RefUserId, cascadeDelete: true)
                .Index(t => t.RefUserId);
            
            CreateTable(
                "dbo.RefDoctorServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefUserId = c.Int(nullable: false),
                        RefServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RefServices", t => t.RefServiceId, cascadeDelete: true)
                .ForeignKey("dbo.RefUsers", t => t.RefUserId, cascadeDelete: true)
                .Index(t => t.RefUserId)
                .Index(t => t.RefServiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefDoctorServices", "RefUserId", "dbo.RefUsers");
            DropForeignKey("dbo.RefDoctorServices", "RefServiceId", "dbo.RefServices");
            DropForeignKey("dbo.RefDoctorSchedules", "RefUserId", "dbo.RefUsers");
            DropForeignKey("dbo.RefDiagnosis", "RefServiceId", "dbo.RefServices");
            DropIndex("dbo.RefDoctorServices", new[] { "RefServiceId" });
            DropIndex("dbo.RefDoctorServices", new[] { "RefUserId" });
            DropIndex("dbo.RefDoctorSchedules", new[] { "RefUserId" });
            DropIndex("dbo.RefDiagnosis", new[] { "RefServiceId" });
            DropTable("dbo.RefDoctorServices");
            DropTable("dbo.RefDoctorSchedules");
            DropTable("dbo.RefDiagnosis");
        }
    }
}
