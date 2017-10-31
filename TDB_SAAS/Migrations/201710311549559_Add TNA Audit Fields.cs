namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTNAAuditFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TNAs", "CreatorID", c => c.Int(nullable: false));
            AddColumn("dbo.TNAs", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.TNAs", "ModifierID", c => c.Int(nullable: false));
            AddColumn("dbo.TNAs", "Modified", c => c.DateTime(nullable: false));
            CreateIndex("dbo.TNAs", "CreatorID");
            CreateIndex("dbo.TNAs", "ModifierID");
            AddForeignKey("dbo.TNAs", "CreatorID", "dbo.Staff", "ID", cascadeDelete: false);
            AddForeignKey("dbo.TNAs", "ModifierID", "dbo.Staff", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TNAs", "ModifierID", "dbo.Staff");
            DropForeignKey("dbo.TNAs", "CreatorID", "dbo.Staff");
            DropIndex("dbo.TNAs", new[] { "ModifierID" });
            DropIndex("dbo.TNAs", new[] { "CreatorID" });
            DropColumn("dbo.TNAs", "Modified");
            DropColumn("dbo.TNAs", "ModifierID");
            DropColumn("dbo.TNAs", "Created");
            DropColumn("dbo.TNAs", "CreatorID");
        }
    }
}
