namespace PGReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PGBedPatientInfoes", "PatientIdType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PGBedPatientInfoes", "PatientIdType");
        }
    }
}
