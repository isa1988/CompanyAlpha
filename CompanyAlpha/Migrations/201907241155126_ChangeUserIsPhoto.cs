namespace CompanyAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserIsPhoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsPhoto", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsPhoto");
        }
    }
}
