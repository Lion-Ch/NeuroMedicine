namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameOrganization = c.String(),
                        fioDirector = c.String(),
                        cityOrgTag = c.String(),
                        addressOrgTag = c.String(),
                        indexOrgTag = c.String(),
                        phoneOrgTag = c.String(),
                        innOrgTag = c.String(),
                        rsOrgTag = c.String(),
                        bankOrgTag = c.String(),
                        ksOrgTag = c.String(),
                        bikOrgTag = c.String(),
                        PathToNeuro = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RefSettings");
        }
    }
}
