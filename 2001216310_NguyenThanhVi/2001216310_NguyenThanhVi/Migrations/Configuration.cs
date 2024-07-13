namespace _2001216310_NguyenThanhVi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<_2001216310_NguyenThanhVi.Models.MyDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "_2001216310_NguyenThanhVi.Models.MyDB";
        }

        protected override void Seed(_2001216310_NguyenThanhVi.Models.MyDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
