namespace Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBasketLine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketLines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BasketID = c.String(),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketLines", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.BasketLines", new[] { "ProductID" });
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.BasketLines");
        }
    }
}
