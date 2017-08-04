namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateStatuses : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT dbo.Status ON");
            Sql("INSERT INTO dbo.Status(ID, Status, Requirement, Attendance, TNA_OUT, RA_PDS, RA_PLUS, RA_ESR, CreatorID, Created, ModifierID, Modified) VALUES (0, ' ', 0, 1, 0, 0, 0, 0, 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Status(ID, Status, Requirement, Attendance, TNA_OUT, RA_PDS, RA_PLUS, RA_ESR, CreatorID, Created, ModifierID, Modified) VALUES (1, 'Required', 1, 0, 0, 0, 0, 0, 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Status(ID, Status, Requirement, Attendance, TNA_OUT, RA_PDS, RA_PLUS, RA_ESR, CreatorID, Created, ModifierID, Modified) VALUES (2, 'Booked', 1, 0, 0, 0, 0, 0, 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Status(ID, Status, Requirement, Attendance, TNA_OUT, RA_PDS, RA_PLUS, RA_ESR, CreatorID, Created, ModifierID, Modified) VALUES (3, 'Did Not Attend', 0, 1, 0, 0, 0, 0, 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Status(ID, Status, Requirement, Attendance, TNA_OUT, RA_PDS, RA_PLUS, RA_ESR, CreatorID, Created, ModifierID, Modified) VALUES (4, 'Uncompleted', 0, 1, 0, 0, 0, 0, 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Status(ID, Status, Requirement, Attendance, TNA_OUT, RA_PDS, RA_PLUS, RA_ESR, CreatorID, Created, ModifierID, Modified) VALUES (5, 'Completed', 1, 1, 0, 0, 0, 0, 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Status(ID, Status, Requirement, Attendance, TNA_OUT, RA_PDS, RA_PLUS, RA_ESR, CreatorID, Created, ModifierID, Modified) VALUES (6, 'Cancelled', 0, 1, 0, 0, 0, 0, 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Status(ID, Status, Requirement, Attendance, TNA_OUT, RA_PDS, RA_PLUS, RA_ESR, CreatorID, Created, ModifierID, Modified) VALUES (7, 'No Longer Required', 1, 1, 0, 0, 0, 0, 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Status(ID, Status, Requirement, Attendance, TNA_OUT, RA_PDS, RA_PLUS, RA_ESR, CreatorID, Created, ModifierID, Modified) VALUES (8, 'Granted', 0, 0, 0, 1, 0, 0, 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Status(ID, Status, Requirement, Attendance, TNA_OUT, RA_PDS, RA_PLUS, RA_ESR, CreatorID, Created, ModifierID, Modified) VALUES (9, 'Not Granted', 0, 0, 0, 1, 0, 0, 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Status(ID, Status, Requirement, Attendance, TNA_OUT, RA_PDS, RA_PLUS, RA_ESR, CreatorID, Created, ModifierID, Modified) VALUES (10, 'No Action Required', 0, 0, 1, 0, 0, 0, 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Status(ID, Status, Requirement, Attendance, TNA_OUT, RA_PDS, RA_PLUS, RA_ESR, CreatorID, Created, ModifierID, Modified) VALUES (11, 'BS1', 0, 0, 1, 0, 0, 0, 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Status(ID, Status, Requirement, Attendance, TNA_OUT, RA_PDS, RA_PLUS, RA_ESR, CreatorID, Created, ModifierID, Modified) VALUES (12, 'BS2', 0, 0, 1, 0, 0, 0, 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Status(ID, Status, Requirement, Attendance, TNA_OUT, RA_PDS, RA_PLUS, RA_ESR, CreatorID, Created, ModifierID, Modified) VALUES (13, 'E-Learning', 0, 0, 1, 0, 0, 0, 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Status(ID, Status, Requirement, Attendance, TNA_OUT, RA_PDS, RA_PLUS, RA_ESR, CreatorID, Created, ModifierID, Modified) VALUES (14, 'Yes', 0, 0, 0, 0, 1, 1, 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Status(ID, Status, Requirement, Attendance, TNA_OUT, RA_PDS, RA_PLUS, RA_ESR, CreatorID, Created, ModifierID, Modified) VALUES (15, 'No', 0, 0, 0, 0, 1, 1, 0, getdate(), 0, getdate())");
            Sql("INSERT INTO dbo.Status(ID, Status, Requirement, Attendance, TNA_OUT, RA_PDS, RA_PLUS, RA_ESR, CreatorID, Created, ModifierID, Modified) VALUES (16, 'Not on ESR', 0, 0, 0, 0, 0, 1, 0, getdate(), 0, getdate())");
            Sql("SET IDENTITY_INSERT dbo.Status OFF");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM dbo.Status");
        }
    }
}
