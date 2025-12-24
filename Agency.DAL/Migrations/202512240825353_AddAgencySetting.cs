namespace QwiikMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAgencySetting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgencySettings",
                c => new
                    {
                        AgencySettingId = c.Int(nullable: false, identity: true),
                        MaxAppointmentsPerDay = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AgencySettingId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AgencySettings");
        }
    }
}
