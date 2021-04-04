namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullablePatientDiagnosis1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RefPatientDiagnosis", "ResultNeuralNetwork", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RefPatientDiagnosis", "ResultNeuralNetwork", c => c.Double());
        }
    }
}
