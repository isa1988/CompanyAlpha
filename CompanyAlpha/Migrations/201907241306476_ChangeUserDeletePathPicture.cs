namespace CompanyAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserDeletePathPicture : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "NamePicture");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "NamePicture", c => c.String(maxLength: 100));
        }
    }
}
