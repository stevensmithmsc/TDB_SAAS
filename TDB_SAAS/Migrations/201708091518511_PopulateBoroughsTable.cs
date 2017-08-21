namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateBoroughsTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO dbo.Boroughs(ID, Borough, Long, Short, CreatorID, Created, ModifierID, Modified) VALUES ('B', 'Bury', 'Bury', 'BUR', 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Boroughs(ID, Borough, Long, Short, CreatorID, Created, ModifierID, Modified) VALUES ('O', 'Oldham', 'Oldham', 'OLD', 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Boroughs(ID, Borough, Long, Short, CreatorID, Created, ModifierID, Modified) VALUES ('R', 'Rochdale', 'Heywood, Middleton & Rochdale', 'HMR', 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Boroughs(ID, Borough, Long, Short, CreatorID, Created, ModifierID, Modified) VALUES ('T', 'Tameside', 'Tameside & Glossop', 'T&G', 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Boroughs(ID, Borough, Long, Short, CreatorID, Created, ModifierID, Modified) VALUES ('S', 'Stockport', 'Stockport', 'STO', 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Boroughs(ID, Borough, Long, Short, CreatorID, Created, ModifierID, Modified) VALUES ('X', 'Trafford', 'Trafford', 'TRF', 0, getdate(), 0, getdate())");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM dbo.Boroughs");
        }
    }
}
