namespace MonoTask.Infrastructure.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EFnavigations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VehicleModelEntities", "VehiceMake_Id", c => c.Int());
            CreateIndex("dbo.VehicleModelEntities", "VehiceMake_Id");
            AddForeignKey("dbo.VehicleModelEntities", "VehiceMake_Id", "dbo.VehicleMakeEntities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleModelEntities", "VehiceMake_Id", "dbo.VehicleMakeEntities");
            DropIndex("dbo.VehicleModelEntities", new[] { "VehiceMake_Id" });
            DropColumn("dbo.VehicleModelEntities", "VehiceMake_Id");
        }
    }
}
