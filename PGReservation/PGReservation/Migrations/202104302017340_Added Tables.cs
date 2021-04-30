namespace PGReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientIdTypes",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.TypeId);
            
            CreateTable(
                "dbo.PatientRecords",
                c => new
                    {
                        PatientRecordsId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.PatientRecordsId);
            
            CreateTable(
                "dbo.PGBedPatientInfoes",
                c => new
                    {
                        PGBedPatientId = c.Int(nullable: false, identity: true),
                        PatientName = c.String(),
                        PatientPhone = c.String(),
                        PatientAddress = c.String(),
                        State = c.String(),
                        District = c.String(),
                        City = c.String(),
                        Pincode = c.String(),
                        PatientIdTypeValue = c.String(),
                        PatientStatus = c.String(),
                        Notes = c.String(),
                        PatientAdmittedOnDate = c.DateTime(nullable: false),
                        PatientDischargedOnDate = c.DateTime(nullable: false),
                        PatientType_TypeId = c.Int(),
                        PgBed_BedID = c.Int(),
                        PatientRecords_PatientRecordsId = c.Int(),
                    })
                .PrimaryKey(t => t.PGBedPatientId)
                .ForeignKey("dbo.PatientIdTypes", t => t.PatientType_TypeId)
                .ForeignKey("dbo.PGBeds", t => t.PgBed_BedID)
                .ForeignKey("dbo.PatientRecords", t => t.PatientRecords_PatientRecordsId)
                .Index(t => t.PatientType_TypeId)
                .Index(t => t.PgBed_BedID)
                .Index(t => t.PatientRecords_PatientRecordsId);
            
            CreateTable(
                "dbo.PGBeds",
                c => new
                    {
                        BedID = c.Int(nullable: false, identity: true),
                        BedNo = c.Int(nullable: false),
                        BedStatus = c.String(),
                        PgRegistration_PGID = c.Int(),
                    })
                .PrimaryKey(t => t.BedID)
                .ForeignKey("dbo.PGRegistrations", t => t.PgRegistration_PGID)
                .Index(t => t.PgRegistration_PGID);
            
            CreateTable(
                "dbo.PGRegistrations",
                c => new
                    {
                        PGID = c.Int(nullable: false, identity: true),
                        PGName = c.String(),
                        ContactPerson = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        State = c.String(),
                        District = c.String(),
                        City = c.String(),
                        PinCode = c.String(),
                        GmapLocation = c.String(),
                        NoOfBeds = c.String(),
                    })
                .PrimaryKey(t => t.PGID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PGBedPatientInfoes", "PatientRecords_PatientRecordsId", "dbo.PatientRecords");
            DropForeignKey("dbo.PGBedPatientInfoes", "PgBed_BedID", "dbo.PGBeds");
            DropForeignKey("dbo.PGBeds", "PgRegistration_PGID", "dbo.PGRegistrations");
            DropForeignKey("dbo.PGBedPatientInfoes", "PatientType_TypeId", "dbo.PatientIdTypes");
            DropIndex("dbo.PGBeds", new[] { "PgRegistration_PGID" });
            DropIndex("dbo.PGBedPatientInfoes", new[] { "PatientRecords_PatientRecordsId" });
            DropIndex("dbo.PGBedPatientInfoes", new[] { "PgBed_BedID" });
            DropIndex("dbo.PGBedPatientInfoes", new[] { "PatientType_TypeId" });
            DropTable("dbo.PGRegistrations");
            DropTable("dbo.PGBeds");
            DropTable("dbo.PGBedPatientInfoes");
            DropTable("dbo.PatientRecords");
            DropTable("dbo.PatientIdTypes");
        }
    }
}
