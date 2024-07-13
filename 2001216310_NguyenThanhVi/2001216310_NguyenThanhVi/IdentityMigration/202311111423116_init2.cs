namespace _2001216310_NguyenThanhVi.IdentityMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UsName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UsName");
        }
    }
}
