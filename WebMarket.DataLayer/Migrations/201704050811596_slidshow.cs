namespace WebMarket.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class slidshow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SlideShows", "BigTitle", c => c.String());
            AddColumn("dbo.SlideShows", "SmallTitle", c => c.String());
            AddColumn("dbo.SlideShows", "Image", c => c.String());
            DropColumn("dbo.SlideShows", "Title");
            DropColumn("dbo.SlideShows", "Picture");
            DropColumn("dbo.SlideShows", "SubPicture1");
            DropColumn("dbo.SlideShows", "SubPicture2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SlideShows", "SubPicture2", c => c.String());
            AddColumn("dbo.SlideShows", "SubPicture1", c => c.String());
            AddColumn("dbo.SlideShows", "Picture", c => c.String());
            AddColumn("dbo.SlideShows", "Title", c => c.String());
            DropColumn("dbo.SlideShows", "Image");
            DropColumn("dbo.SlideShows", "SmallTitle");
            DropColumn("dbo.SlideShows", "BigTitle");
        }
    }
}
