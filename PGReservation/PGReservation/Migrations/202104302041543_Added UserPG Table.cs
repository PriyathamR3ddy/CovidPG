namespace PGReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserPGTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPGs",
                c => new
                    {
                        UserPGId = c.Int(nullable: false, identity: true),
                        user_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserPGId)
                .ForeignKey("dbo.AspNetUsers", t => t.user_Id)
                .Index(t => t.user_Id);
            
            AddColumn("dbo.PGRegistrations", "UserPG_UserPGId", c => c.Int());
            CreateIndex("dbo.PGRegistrations", "UserPG_UserPGId");
            AddForeignKey("dbo.PGRegistrations", "UserPG_UserPGId", "dbo.UserPGs", "UserPGId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPGs", "user_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PGRegistrations", "UserPG_UserPGId", "dbo.UserPGs");
            DropIndex("dbo.UserPGs", new[] { "user_Id" });
            DropIndex("dbo.PGRegistrations", new[] { "UserPG_UserPGId" });
            DropColumn("dbo.PGRegistrations", "UserPG_UserPGId");
            DropTable("dbo.UserPGs");
        }
    }
}
