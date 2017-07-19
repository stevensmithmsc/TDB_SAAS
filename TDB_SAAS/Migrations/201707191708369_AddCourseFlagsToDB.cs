namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseFlagsToDB : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO dbo.CFlags(ID, Description, CreatorID, Created, ModifierID, Modified) VALUES ('PAR', 'Paris', 0, getdate(), 0, getdate() )");
            Sql("INSERT INTO dbo.CFlags(ID, Description, CreatorID, Created, ModifierID, Modified) VALUES ('CHH', 'Child Health', 0, getdate(), 0, getdate() )");
            Sql("INSERT INTO dbo.CFlags(ID, Description, CreatorID, Created, ModifierID, Modified) VALUES ('OBS', 'Obselete', 0, getdate(), 0, getdate() )");
            Sql("INSERT INTO dbo.CFlags(ID, Description, CreatorID, Created, ModifierID, Modified) VALUES ('BAU', 'B.A.U.', 0, getdate(), 0, getdate() )");
            Sql("INSERT INTO dbo.CFlags(ID, Description, CreatorID, Created, ModifierID, Modified) VALUES ('EXT', 'External', 0, getdate(), 0, getdate() )");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM dbo.CFlags");
        }
    }
}
