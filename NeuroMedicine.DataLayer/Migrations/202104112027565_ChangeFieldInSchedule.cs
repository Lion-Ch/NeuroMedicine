namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFieldInSchedule : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefDoctorSchedules", "TimeStart", c => c.String());
            AddColumn("dbo.RefDoctorSchedules", "TimeEnd", c => c.String());
            DropColumn("dbo.RefDoctorSchedules", "DateStart");
            DropColumn("dbo.RefDoctorSchedules", "DateEnd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RefDoctorSchedules", "DateEnd", c => c.DateTime(nullable: false));
            AddColumn("dbo.RefDoctorSchedules", "DateStart", c => c.DateTime(nullable: false));
            DropColumn("dbo.RefDoctorSchedules", "TimeEnd");
            DropColumn("dbo.RefDoctorSchedules", "TimeStart");
        }
    }
}
