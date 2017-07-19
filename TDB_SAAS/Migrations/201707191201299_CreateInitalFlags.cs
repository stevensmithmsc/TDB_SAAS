namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateInitalFlags : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO dbo.Flags(ID, Description, CreatorID, Created, ModifierID, Modified) VALUES ('TRN', 'Trainer', 0, getdate(), 0, getdate() )");
            Sql("INSERT INTO dbo.Flags(ID, Description, CreatorID, Created, ModifierID, Modified) VALUES ('LMR', 'Line Manager', 0, getdate(), 0, getdate() )");
            Sql("INSERT INTO dbo.Flags(ID, Description, CreatorID, Created, ModifierID, Modified) VALUES ('LTR', 'Left Trust', 0, getdate(), 0, getdate() )");
            Sql("INSERT INTO dbo.Flags(ID, Description, CreatorID, Created, ModifierID, Modified) VALUES ('NTR', 'No Training Required', 0, getdate(), 0, getdate() )");
            Sql("INSERT INTO dbo.Flags(ID, Description, CreatorID, Created, ModifierID, Modified) VALUES ('BAN', 'Bank', 0, getdate(), 0, getdate() )");
            Sql("INSERT INTO dbo.Flags(ID, Description, CreatorID, Created, ModifierID, Modified) VALUES ('EXT', 'External', 0, getdate(), 0, getdate() )");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM dbo.Flags");
        }
    }
}
