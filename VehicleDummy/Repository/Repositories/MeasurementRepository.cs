using Microsoft.EntityFrameworkCore;
using VehicleDummy.Models;
using VehicleDummy.Repository.Interfaces;

namespace VehicleDummy.Repository.Repositories
{
    public class MeasurementRepository : IMeasurementRepository
    {
        private readonly VehicleDummyDbContext _dbContext;

        public MeasurementRepository(VehicleDummyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Measurement> CreateMeasurement(Measurement measurement)
        {
            _dbContext.Measurements.Add(measurement);
            await _dbContext.SaveChangesAsync();
            return measurement;
        }

        public async Task<List<Measurement>> CreateMeasurementList(List<Measurement> measurements)
        {
            _dbContext.Measurements.AddRangeAsync(measurements);
            await _dbContext.SaveChangesAsync();
            return measurements;
        }

        public async Task DeleteMeasurementAsync(Measurement measurement)
        {
            _dbContext.Measurements.Remove(measurement);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Measurement>> GetAllMeasurementAsync()
        {
            return await _dbContext.Measurements.ToListAsync();
        }

        public async Task<List<Measurement>> GetAllMeasurementByDateAsync(DateTime measurementDate)
        {
            return await _dbContext.Measurements.Where(measurement => measurement.Date.Date == measurementDate.Date).ToListAsync();
        }

        public async Task<Measurement> GetMeasurementByIdAsync(int measurementId)
        {
            return await _dbContext.Measurements.FirstOrDefaultAsync(measurement => measurement.Id == measurementId);
        }

        public async Task<List<Measurement>> GetMeasurementListByMeasurementPointId(int measurementId)
        {
            return await _dbContext.Measurements.Where(measurement => measurement.MeasurementPointId == measurementId).ToListAsync();
        }

        public async Task<List<Measurement>> GetMeasurementListByShopId(int id)
        {
            return await _dbContext.Measurements.Where(measurement => measurement.ShopId == id).ToListAsync();
        }

        public async Task<List<Measurement>> GetMeasurementListByVehicleId(int vehicleId)
        {
            return await _dbContext.Measurements.Where(measurement => measurement.VehicleId == vehicleId).ToListAsync();
        }

        public async Task<List<Measurement>> GetTopMeasurementsAsnyc(int count)
        {
            return await _dbContext.Measurements.Take(count).ToListAsync();
        }

        public async Task<Measurement> UpdateMeasurementAsync(Measurement measurement)
        {
            _dbContext.Measurements.Update(measurement);
            await _dbContext.SaveChangesAsync();
            return measurement;
        }
    }
}
