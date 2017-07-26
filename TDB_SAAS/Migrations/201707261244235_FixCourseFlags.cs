namespace TDB_SAAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCourseFlags : DbMigration
    {
        public override void Up()
        {
            Sql("SELECT * into dbo.flagtemp from dbo.CFlags");
            Sql("SELECT * into dbo.courseflagtemp from dbo.CourseFlags");
            Sql("Truncate Table dbo.CourseFlags");
            DropForeignKey("dbo.CourseFlags", "Flag", "dbo.CFlags");
            Sql("Truncate Table dbo.CFlags");
            DropIndex("dbo.CourseFlags", new[] { "Flag" });
            DropPrimaryKey("dbo.CFlags");
            DropPrimaryKey("dbo.CourseFlags");
            AddColumn("dbo.CFlags", "Code", c => c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false));
            AlterColumn("dbo.CFlags", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.CourseFlags", "Flag", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CFlags", "ID");
            AddPrimaryKey("dbo.CourseFlags", new[] { "CourseID", "Flag" });
            CreateIndex("dbo.CFlags", "Code");
            CreateIndex("dbo.CourseFlags", "Flag");
            AddForeignKey("dbo.CourseFlags", "Flag", "dbo.CFlags", "ID", cascadeDelete: true);
            Sql("INSERT INTO dbo.CFlags (ID, Code, Description, CreatorID, Created, ModifierID, Modified) SELECT ROW_NUMBER() OVER(ORDER BY ID), * FROM dbo.flagtemp");
            Sql("INSERT INTO dbo.CourseFlags SELECT CourseID, CFlags.ID FROM dbo.courseflagtemp JOIN dbo.CFlags on courseflagtemp.Flag = CFlags.Code");
            Sql("Drop Table dbo.flagtemp");
            Sql("Drop Table dbo.courseflagtemp");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseFlags", "Flag", "dbo.CFlags");
            DropIndex("dbo.CourseFlags", new[] { "Flag" });
            DropIndex("dbo.CFlags", new[] { "Code" });
            DropPrimaryKey("dbo.CourseFlags");
            DropPrimaryKey("dbo.CFlags");
            AlterColumn("dbo.CourseFlags", "Flag", c => c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false));
            AlterColumn("dbo.CFlags", "ID", c => c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false));
            DropColumn("dbo.CFlags", "Code");
            AddPrimaryKey("dbo.CourseFlags", new[] { "CourseID", "Flag" });
            AddPrimaryKey("dbo.CFlags", "ID");
            CreateIndex("dbo.CourseFlags", "Flag");
            AddForeignKey("dbo.CourseFlags", "Flag", "dbo.CFlags", "ID", cascadeDelete: true);
        }
    }
}
