namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        DriverID = c.Int(nullable: false, identity: true),
                        DriverName = c.String(),
                        Age = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.DriverID);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        TripID = c.Int(nullable: false, identity: true),
                        VehicleID = c.Int(nullable: false),
                        DriverID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TripID)
                .ForeignKey("dbo.Drivers", t => t.DriverID, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleID, cascadeDelete: true)
                .Index(t => t.VehicleID)
                .Index(t => t.DriverID);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleID = c.Int(nullable: false, identity: true),
                        VehicleName = c.String(),
                    })
                .PrimaryKey(t => t.VehicleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trips", "VehicleID", "dbo.Vehicles");
            DropForeignKey("dbo.Trips", "DriverID", "dbo.Drivers");
            DropIndex("dbo.Trips", new[] { "DriverID" });
            DropIndex("dbo.Trips", new[] { "VehicleID" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.Trips");
            DropTable("dbo.Drivers");
        }
    }
}
