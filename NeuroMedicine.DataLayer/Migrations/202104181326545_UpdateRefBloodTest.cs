namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRefBloodTest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefBloodTests", "E", c => c.Double(nullable: false));
            AlterColumn("dbo.RefBloodTests", "Coagulability", c => c.Double());
            AlterColumn("dbo.RefBloodTests", "Platelets", c => c.Double());
            AlterColumn("dbo.RefBloodTests", "ProthrombinIndex", c => c.Double());
            AlterColumn("dbo.RefBloodTests", "B", c => c.Double());
            AlterColumn("dbo.RefBloodTests", "YU", c => c.Double());
            DropColumn("dbo.RefBloodTests", "ChangeErythrocytes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RefBloodTests", "ChangeErythrocytes", c => c.Double(nullable: false));
            AlterColumn("dbo.RefBloodTests", "YU", c => c.Double(nullable: false));
            AlterColumn("dbo.RefBloodTests", "B", c => c.Double(nullable: false));
            AlterColumn("dbo.RefBloodTests", "ProthrombinIndex", c => c.Double(nullable: false));
            AlterColumn("dbo.RefBloodTests", "Platelets", c => c.Double(nullable: false));
            AlterColumn("dbo.RefBloodTests", "Coagulability", c => c.Double(nullable: false));
            DropColumn("dbo.RefBloodTests", "E");
        }
    }
}
