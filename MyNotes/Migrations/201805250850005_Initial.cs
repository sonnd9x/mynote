namespace MyNotes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_Notes",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Content = c.String(storeType: "ntext"),
                        CreateDate = c.DateTime(),
                        Public = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tbl_User",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Username = c.String(maxLength: 50, unicode: false),
                        Password = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tbl_User");
            DropTable("dbo.tbl_Notes");
        }
    }
}
