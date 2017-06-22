namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialData : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT dbo.Staff ON");
            Sql("INSERT INTO dbo.Staff(ID, FName, SName, PName, Comments, Created, Modified) VALUES (0, 'Developer', 'User', 'Admin', 'Account user when developer needs to manipulate data', getdate(), getdate())");
            Sql("SET IDENTITY_INSERT dbo.Staff OFF");
            Sql("INSERT INTO dbo.Titles(Title, Genders, DefaultGender, CreatorID, Created, ModifierID, Modified) VALUES ('Dr', '1;2;3', 0, 0, getdate(), 0, getdate() )");
            Sql("INSERT INTO dbo.Titles(Title, Genders, DefaultGender, CreatorID, Created, ModifierID, Modified) VALUES ('Mr', '1;2;3', 1, 0, getdate(), 0, getdate() )");
            Sql("INSERT INTO dbo.Titles(Title, Genders, DefaultGender, CreatorID, Created, ModifierID, Modified) VALUES ('Mrs', '2;3', 2, 0, getdate(), 0, getdate() )");
            Sql("INSERT INTO dbo.Titles(Title, Genders, DefaultGender, CreatorID, Created, ModifierID, Modified) VALUES ('Miss', '2;3', 2, 0, getdate(), 0, getdate() )");
            Sql("INSERT INTO dbo.Titles(Title, Genders, DefaultGender, CreatorID, Created, ModifierID, Modified) VALUES ('Ms', '2;3', 2, 0, getdate(), 0, getdate() )");    
        }
        
        public override void Down()
        {
            Sql("DELETE FROM dbo.Staff WHERE ID = 0");
            Sql("DELETE FROM dbo.Titles");
        }
    }
}
