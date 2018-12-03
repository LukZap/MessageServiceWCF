namespace MessageService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeIdToMessageId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Messages");
            AddColumn("dbo.Messages", "MessageId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Messages", "MessageId");
            DropColumn("dbo.Messages", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Id", c => c.Guid(nullable: false, identity: true));
            DropPrimaryKey("dbo.Messages");
            DropColumn("dbo.Messages", "MessageId");
            AddPrimaryKey("dbo.Messages", "Id");
        }
    }
}
