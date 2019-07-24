namespace CompanyAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderRooms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        UserID = c.Int(nullable: false),
                        RoomID = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Rooms", t => t.RoomID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.RoomID);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        SeatsCount = c.Int(nullable: false),
                        IsProjector = c.Boolean(nullable: false),
                        IsMarkerBoard = c.Boolean(nullable: false),
                        IsBlock = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        RoleID = c.Int(nullable: false),
                        Name = c.String(maxLength: 100),
                        SurName = c.String(maxLength: 100),
                        MiddleName = c.String(maxLength: 100),
                        IsBlock = c.Boolean(nullable: false),
                        NamePicture = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.RoleID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        IsChangeRoom = c.Boolean(nullable: false),
                        IsEditUser = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderRooms", "UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.OrderRooms", "RoomID", "dbo.Rooms");
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.OrderRooms", new[] { "RoomID" });
            DropIndex("dbo.OrderRooms", new[] { "UserID" });
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Rooms");
            DropTable("dbo.OrderRooms");
        }
    }
}
