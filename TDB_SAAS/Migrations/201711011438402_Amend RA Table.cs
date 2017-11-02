namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AmendRATable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RAs", "UUID", c => c.String(maxLength: 12, unicode: false));
            DropColumn("dbo.RAs", "PA1Rec");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RAs", "PA1Rec", c => c.DateTime());
            AlterColumn("dbo.RAs", "UUID", c => c.String());
        }
    }
}
