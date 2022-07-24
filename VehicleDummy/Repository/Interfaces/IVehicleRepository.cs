using VehicleDummy.Models;

namespace VehicleDummy.Repository.Interfaces
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetAllVehicleAsync();
        Task<Vehicle> GetVehicleByIdAsync(int vehicleId);
        Task<Vehicle> GetVehicleByJSNAsync(string vehicleJSN);
        Task<List<Vehicle>> GetAllVehicleByModelAsync(string vehicleModel);
        Task<Vehicle> CreateVehicleAsync(Vehicle vehicle);
        Task<List<Vehicle>> CreateVehicleListAsync(List<Vehicle> vehicleList);
        Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(Vehicle vehicle);
    }
}
