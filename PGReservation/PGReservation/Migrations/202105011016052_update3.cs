namespace PGReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PGRegistrations", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PGRegistrations", new[] { "User_Id" });
            AddColumn("dbo.PGRegistrations", "UserId", c => c.String());
            DropColumn("dbo.PGRegistrations", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PGRegistrations", "User_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.PGRegistrations", "UserId");
            CreateIndex("dbo.PGRegistrations", "User_Id");
            AddForeignKey("dbo.PGRegistrations", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
