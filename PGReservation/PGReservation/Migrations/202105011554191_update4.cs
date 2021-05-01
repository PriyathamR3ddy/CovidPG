namespace PGReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PGBedPatientInfoes", "EmergencyContact", c => c.String());
            AddColumn("dbo.PGBedPatientInfoes", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PGBedPatientInfoes", "Gender");
            DropColumn("dbo.PGBedPatientInfoes", "EmergencyContact");
        }
    }
}
