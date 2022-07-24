using VehicleDummy.Models;

namespace VehicleDummy.Repository.Interfaces
{
    public interface IMeasurementPointRepository
    {
        Task<List<MeasurementPoint>> GetAllMeasurementPointAsync();
        Task<MeasurementPoint> GetMeasurementPointByIdAsync(int measurementPointId);
        Task<MeasurementPoint> GetMeasurementPointByNameAsync(string measurementPointName);
        Task<MeasurementPoint> CreateMeasurementPointAsync(MeasurementPoint measurementPoint);
        Task<List<MeasurementPoint>> CreateMeasurementPointListAsync(List<MeasurementPoint> measurementPoints);
        Task<MeasurementPoint> UpdateMeasurementPointAsync(MeasurementPoint measurementPoint);
        Task DeleteMeasurementPointAsync(MeasurementPoint measurementPoint);
        
    }
}
