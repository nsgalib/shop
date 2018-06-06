namespace Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecategory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String());
        }
    }
}
