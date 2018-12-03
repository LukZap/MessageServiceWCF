namespace MessageService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIdentityForId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Messages");
            AlterColumn("dbo.Messages", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Messages", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Messages");
            AlterColumn("dbo.Messages", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Messages", "Id");
        }
    }
}
