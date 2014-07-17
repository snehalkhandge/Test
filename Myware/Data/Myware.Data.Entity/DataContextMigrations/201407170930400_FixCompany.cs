namespace Myware.Data.Entity.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCompany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Developers", "Company_Id", "dbo.Companies");
            DropIndex("dbo.Developers", new[] { "Company_Id" });
            AddColumn("dbo.Companies", "Developer_Id", c => c.Int());
            CreateIndex("dbo.Companies", "Developer_Id");
            AddForeignKey("dbo.Companies", "Developer_Id", "dbo.Developers", "Id");
            DropColumn("dbo.Developers", "Company_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Developers", "Company_Id", c => c.Int());
            DropForeignKey("dbo.Companies", "Developer_Id", "dbo.Developers");
            DropIndex("dbo.Companies", new[] { "Developer_Id" });
            DropColumn("dbo.Companies", "Developer_Id");
            CreateIndex("dbo.Developers", "Company_Id");
            AddForeignKey("dbo.Developers", "Company_Id", "dbo.Companies", "Id");
        }
    }
}
