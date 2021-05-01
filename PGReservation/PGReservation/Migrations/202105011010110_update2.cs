namespace PGReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PGRegistrations", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.PGRegistrations", "User_Id");
            AddForeignKey("dbo.PGRegistrations", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PGRegistrations", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PGRegistrations", new[] { "User_Id" });
            DropColumn("dbo.PGRegistrations", "User_Id");
        }
    }
}
