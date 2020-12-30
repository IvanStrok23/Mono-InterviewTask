namespace MonoTask.Infrastructure.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Entitysufix : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.VehicleMakes", newName: "VehicleMakeEntities");
            RenameTable(name: "dbo.VehicleModels", newName: "VehicleModelEntities");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.VehicleModelEntities", newName: "VehicleModels");
            RenameTable(name: "dbo.VehicleMakeEntities", newName: "VehicleMakes");
        }
    }
}
