namespace WebMarket.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SlideShows", "Picture", c => c.String());
            AddColumn("dbo.SlideShows", "SubPicture1", c => c.String());
            AddColumn("dbo.SlideShows", "SubPicture2", c => c.String());
            DropColumn("dbo.SlideShows", "KeyWord");
            DropColumn("dbo.SlideShows", "Picture1");
            DropColumn("dbo.SlideShows", "SubPicture11");
            DropColumn("dbo.SlideShows", "SubPicture12");
            DropColumn("dbo.SlideShows", "Picture2");
            DropColumn("dbo.SlideShows", "SubPicture21");
            DropColumn("dbo.SlideShows", "SubPicture22");
            DropColumn("dbo.SlideShows", "Picture3");
            DropColumn("dbo.SlideShows", "SubPicture31");
            DropColumn("dbo.SlideShows", "SubPicture32");
            DropColumn("dbo.SlideShows", "Picture4");
            DropColumn("dbo.SlideShows", "SubPicture41");
            DropColumn("dbo.SlideShows", "SubPicture42");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SlideShows", "SubPicture42", c => c.String());
            AddColumn("dbo.SlideShows", "SubPicture41", c => c.String());
            AddColumn("dbo.SlideShows", "Picture4", c => c.String());
            AddColumn("dbo.SlideShows", "SubPicture32", c => c.String());
            AddColumn("dbo.SlideShows", "SubPicture31", c => c.String());
            AddColumn("dbo.SlideShows", "Picture3", c => c.String());
            AddColumn("dbo.SlideShows", "SubPicture22", c => c.String());
            AddColumn("dbo.SlideShows", "SubPicture21", c => c.String());
            AddColumn("dbo.SlideShows", "Picture2", c => c.String());
            AddColumn("dbo.SlideShows", "SubPicture12", c => c.String());
            AddColumn("dbo.SlideShows", "SubPicture11", c => c.String());
            AddColumn("dbo.SlideShows", "Picture1", c => c.String());
            AddColumn("dbo.SlideShows", "KeyWord", c => c.String());
            DropColumn("dbo.SlideShows", "SubPicture2");
            DropColumn("dbo.SlideShows", "SubPicture1");
            DropColumn("dbo.SlideShows", "Picture");
        }
    }
}
