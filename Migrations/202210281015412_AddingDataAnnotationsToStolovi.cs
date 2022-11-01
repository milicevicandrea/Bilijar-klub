namespace BilijarKlub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDataAnnotationsToStolovi : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stoes", "BilijarskiSto", c => c.String(nullable: false));
            AlterColumn("dbo.Stoes", "Vreme", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stoes", "Vreme", c => c.String());
            AlterColumn("dbo.Stoes", "BilijarskiSto", c => c.String());
        }
    }
}
