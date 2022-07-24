using Microsoft.EntityFrameworkCore;
using VehicleDummy.Models;
using VehicleDummy.Repository.Interfaces;

namespace VehicleDummy.Repository.Repositories
{
    public class MeasurementPointRepository : IMeasurementPointRepository
    {
        private readonly VehicleDummyDbContext _dbContext;

        public MeasurementPointRepository(VehicleDummyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MeasurementPoint> CreateMeasurementPointAsync(MeasurementPoint measurementPoint)
        {
            _dbContext.MeasurementPoints.Add(measurementPoint);
            await _dbContext.SaveChangesAsync();
            return measurementPoint;
        }

        public async Task<List<MeasurementPoint>> CreateMeasurementPointListAsync(List<MeasurementPoint> measurementPoints)
        {
            _dbContext.MeasurementPoints.AddRangeAsync(measurementPoints);
            _dbContext.SaveChangesAsync();
            return measurementPoints;
        }

        public async Task DeleteMeasurementPointAsync(MeasurementPoint measurementPoint)
        {
            _dbContext.MeasurementPoints.Remove(measurementPoint);
            _dbContext.SaveChanges();
        }

        public async Task<List<MeasurementPoint>> GetAllMeasurementPointAsync()
        {
            return await _dbContext.MeasurementPoints.ToListAsync();
        }

        public async Task<MeasurementPoint> GetMeasurementPointByIdAsync(int measurementPointId)
        {
            return await _dbContext.MeasurementPoints.FirstOrDefaultAsync(measurementPoint => measurementPointId == measurementPoint.MeasurementPointId);
        }

        public async Task<MeasurementPoint> GetMeasurementPointByNameAsync(string measurementPointName)
        {
            return await _dbContext.MeasurementPoints.FirstOrDefaultAsync(measurementPoint => measurementPoint.Name == measurementPointName);
        }

        public async Task<MeasurementPoint> UpdateMeasurementPointAsync(MeasurementPoint measurementPoint)
        {
            _dbContext.MeasurementPoints.Update(measurementPoint);
            _dbContext.SaveChangesAsync();
            return measurementPoint;
        }
    }
}
