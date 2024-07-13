namespace _2001216310_NguyenThanhVi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UDPro : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Price", c => c.Double());
        }
    }
}
