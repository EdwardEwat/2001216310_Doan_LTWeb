namespace _2001216310_NguyenThanhVi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Img = c.String(),
                        Salary = c.Double(),
                        Islocked = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        IsGuest = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        Total = c.Double(),
                        Description = c.String(),
                        IsPay = c.Boolean(),
                        StoreId = c.Int(),
                        AccountId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .ForeignKey("dbo.Stores", t => t.StoreId)
                .Index(t => t.StoreId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Buyings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillId = c.Int(),
                        ProductId = c.Int(),
                        Amount = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.BillId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.BillId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Double(),
                        BfDiscount = c.Double(),
                        Type = c.String(),
                        Status = c.String(),
                        PromotionId = c.Int(),
                        BrandId = c.Int(),
                        Img = c.String(),
                        Storages_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Promotions", t => t.PromotionId)
                .ForeignKey("dbo.Storages", t => t.Storages_Id)
                .Index(t => t.PromotionId)
                .Index(t => t.BrandId)
                .Index(t => t.Storages_Id);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Discount = c.Double(),
                        Img = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Amount = c.Int(),
                        StoreId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Locate = c.String(),
                        OwnerStore = c.String(),
                        Img = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Img = c.String(),
                        StoreId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Storages", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Owners", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Bills", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Products", "Storages_Id", "dbo.Storages");
            DropForeignKey("dbo.Products", "PromotionId", "dbo.Promotions");
            DropForeignKey("dbo.Buyings", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Buyings", "BillId", "dbo.Bills");
            DropForeignKey("dbo.Bills", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Owners", new[] { "StoreId" });
            DropIndex("dbo.Storages", new[] { "StoreId" });
            DropIndex("dbo.Products", new[] { "Storages_Id" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropIndex("dbo.Products", new[] { "PromotionId" });
            DropIndex("dbo.Buyings", new[] { "ProductId" });
            DropIndex("dbo.Buyings", new[] { "BillId" });
            DropIndex("dbo.Bills", new[] { "AccountId" });
            DropIndex("dbo.Bills", new[] { "StoreId" });
            DropTable("dbo.Owners");
            DropTable("dbo.Stores");
            DropTable("dbo.Storages");
            DropTable("dbo.Promotions");
            DropTable("dbo.Brands");
            DropTable("dbo.Products");
            DropTable("dbo.Buyings");
            DropTable("dbo.Bills");
            DropTable("dbo.Accounts");
        }
    }
}
