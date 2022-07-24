using Microsoft.EntityFrameworkCore;
using VehicleDummy.Services;

namespace VehicleDummy.Models
{
    public class VehicleDummyDbContext : DbContext
    {
        public VehicleDummyDbContext(DbContextOptions<VehicleDummyDbContext> options)
        : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    DataSeedingService seeder = new();
        //    List<Shop> shops = seeder.GenerateShopList();
        //    List<Vehicle> vehicles = seeder.GenerateVehicleList();
        //    List<MeasurementPoint> measurementPoints = seeder.GenerateMeasurementPointList();

        //    modelBuilder.Entity<Shop>().HasData(shops);
        //    modelBuilder.Entity<Vehicle>().HasData(vehicles);
        //    modelBuilder.Entity<MeasurementPoint>().HasData(measurementPoints);
        //    for (int i = 1; i <= 5; i++)
        //    {
        //        int startId = i == 1 ? 1 : i*1000+i;
        //        List<Measurement> measurements = seeder.GenerateMeasurementList(startId: startId);
        //        modelBuilder.Entity<Measurement>().HasData(measurements);
        //    }
        //}

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<MeasurementPoint> MeasurementPoints { get; set; }
    }
}
