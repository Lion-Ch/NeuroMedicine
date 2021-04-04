namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDatePhotoPatientDiagnosis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefPatientDiagnosis", "DatePhoto", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefPatientDiagnosis", "DatePhoto");
        }
    }
}
