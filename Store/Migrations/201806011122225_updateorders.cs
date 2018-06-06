namespace Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateorders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "UserID");
        }
    }
}
