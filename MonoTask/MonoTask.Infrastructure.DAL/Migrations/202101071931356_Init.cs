namespace MonoTask.Infrastructure.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehicleMakeEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VehicleModelEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Year = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        VehiceMake_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VehicleMakeEntities", t => t.VehiceMake_Id)
                .Index(t => t.VehiceMake_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleModelEntities", "VehiceMake_Id", "dbo.VehicleMakeEntities");
            DropIndex("dbo.VehicleModelEntities", new[] { "VehiceMake_Id" });
            DropTable("dbo.VehicleModelEntities");
            DropTable("dbo.VehicleMakeEntities");
        }
    }
}
