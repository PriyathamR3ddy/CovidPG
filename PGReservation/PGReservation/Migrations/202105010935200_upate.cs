namespace PGReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upate : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UserPGs", name: "user_Id", newName: "UserId");
            RenameIndex(table: "dbo.UserPGs", name: "IX_user_Id", newName: "IX_UserId");
            AddColumn("dbo.PGRegistrations", "FirstName", c => c.String());
            AddColumn("dbo.PGRegistrations", "LastName", c => c.String());
            AddColumn("dbo.PGRegistrations", "Email", c => c.String());
            AddColumn("dbo.PGRegistrations", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.UserPGs", "PGID", c => c.Int(nullable: false));
            AlterColumn("dbo.PGBeds", "BedNo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PGBeds", "BedNo", c => c.Int(nullable: false));
            DropColumn("dbo.UserPGs", "PGID");
            DropColumn("dbo.PGRegistrations", "Discriminator");
            DropColumn("dbo.PGRegistrations", "Email");
            DropColumn("dbo.PGRegistrations", "LastName");
            DropColumn("dbo.PGRegistrations", "FirstName");
            RenameIndex(table: "dbo.UserPGs", name: "IX_UserId", newName: "IX_user_Id");
            RenameColumn(table: "dbo.UserPGs", name: "UserId", newName: "user_Id");
        }
    }
}
