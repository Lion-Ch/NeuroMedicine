namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullableMeansurment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RefMeasurments", "Height", c => c.Single());
            AlterColumn("dbo.RefMeasurments", "Weight", c => c.Single());
            AlterColumn("dbo.RefMeasurments", "Pulse", c => c.Int());
            AlterColumn("dbo.RefMeasurments", "PressureSystolic", c => c.Int());
            AlterColumn("dbo.RefMeasurments", "PressureDiastolic", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RefMeasurments", "PressureDiastolic", c => c.Int(nullable: false));
            AlterColumn("dbo.RefMeasurments", "PressureSystolic", c => c.Int(nullable: false));
            AlterColumn("dbo.RefMeasurments", "Pulse", c => c.Int(nullable: false));
            AlterColumn("dbo.RefMeasurments", "Weight", c => c.Single(nullable: false));
            AlterColumn("dbo.RefMeasurments", "Height", c => c.Single(nullable: false));
        }
    }
}
