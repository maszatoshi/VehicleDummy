using Microsoft.EntityFrameworkCore;
using VehicleDummy.Models;
using VehicleDummy.Repository.Interfaces;

namespace VehicleDummy.Repository.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VehicleDummyDbContext _dbContext;
        public VehicleRepository(VehicleDummyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Vehicle> CreateVehicleAsync(Vehicle vehicle)
        {
            _dbContext.Vehicles.Add(vehicle);
            _dbContext.SaveChangesAsync();
            return vehicle;
        }

        public async Task<List<Vehicle>> CreateVehicleListAsync(List<Vehicle> vehicleList)
        {
            _dbContext.Vehicles.AddRange(vehicleList);
            await _dbContext.SaveChangesAsync();
            return vehicleList;
        }

        public async Task DeleteVehicleAsync(Vehicle vehicle)
        {
            _dbContext.Vehicles.Remove(vehicle);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Vehicle>> GetAllVehicleAsync()
        {
            return await _dbContext.Vehicles.ToListAsync();
        }

        public async Task<List<Vehicle>> GetAllVehicleByModelAsync(string vehicleModel)
        {
            return await _dbContext.Vehicles.Where(x => x.VehicleModel == vehicleModel).ToListAsync();
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int vehicleId)
        {
            return await _dbContext.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.VehicleId == vehicleId);
        }

        public async Task<Vehicle> GetVehicleByJSNAsync(string vehicleJSN)
        {
            return await _dbContext.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.JSN == vehicleJSN);
        }

        public async Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle)
        {
            _dbContext.Vehicles.Update(vehicle);
            await _dbContext.SaveChangesAsync();
            return vehicle;
        }
    }
}
