namespace MonoTask.Infrastructure.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Testnav : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.VehicleModelEntities", "MakeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VehicleModelEntities", "MakeId", c => c.Int(nullable: false));
        }
    }
}
