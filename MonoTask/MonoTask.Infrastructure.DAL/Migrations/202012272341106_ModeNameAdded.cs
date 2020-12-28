namespace MonoTask.Infrastructure.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModeNameAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VehicleModels", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.VehicleModels", "Name");
        }
    }
}
