namespace MVCApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kategorie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ksiazki",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tytuł = c.String(),
                        ISBN = c.String(),
                        Cena = c.Int(nullable: false),
                        Kategoria_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kategorie", t => t.Kategoria_Id)
                .Index(t => t.Kategoria_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ksiazki", "Kategoria_Id", "dbo.Kategorie");
            DropIndex("dbo.Ksiazki", new[] { "Kategoria_Id" });
            DropTable("dbo.Ksiazki");
            DropTable("dbo.Kategorie");
        }
    }
}
