namespace QwiikMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPublicHoliday : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PublicHolidays",
                c => new
                    {
                        PublicHolidayId = c.Int(nullable: false, identity: true),
                        HolidayDate = c.DateTime(nullable: false),
                        Reason = c.String(),
                    })
                .PrimaryKey(t => t.PublicHolidayId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PublicHolidays");
        }
    }
}
