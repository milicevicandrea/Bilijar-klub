namespace BilijarKlub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BilijarskiSto = c.String(),
                        Datum = c.DateTime(nullable: false),
                        Vreme = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Stoes");
        }
    }
}
