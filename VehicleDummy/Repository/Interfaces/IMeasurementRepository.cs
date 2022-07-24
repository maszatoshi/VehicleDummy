using VehicleDummy.Models;

namespace VehicleDummy.Repository.Interfaces
{
    public interface IMeasurementRepository
    {
        Task<List<Measurement>> GetAllMeasurementAsync();
        Task<List<Measurement>> GetTopMeasurementsAsnyc(int count);
        Task<Measurement> GetMeasurementByIdAsync(int measurementId);
        Task<List<Measurement>> GetMeasurementListByVehicleId(int vehicleId);
        Task<List<Measurement>> GetMeasurementListByShopId(int id);
        Task<List<Measurement>> GetMeasurementListByMeasurementPointId(int measurementId);
        Task<List<Measurement>> GetAllMeasurementByDateAsync(DateTime measurementDate);
        Task<Measurement> CreateMeasurement(Measurement measurement);
        Task<List<Measurement>> CreateMeasurementList(List<Measurement> measurements);
        Task<Measurement> UpdateMeasurementAsync(Measurement measurement);
        Task DeleteMeasurementAsync(Measurement measurement);
    }
}
