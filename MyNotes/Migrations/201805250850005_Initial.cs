namespace MyNotes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_Emails",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Subject = c.String(maxLength: 4000),
                        From = c.String(),
                        To = c.String(),
                        Text = c.String(maxLength: 4000),
                        Html = c.String(maxLength: 4000),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

        }
        
        public override void Down()
        {
            DropTable("dbo.tbl_Emails");
        }
    }
}
