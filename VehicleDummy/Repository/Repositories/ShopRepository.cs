using Microsoft.EntityFrameworkCore;
using VehicleDummy.Models;
using VehicleDummy.Repository.Interfaces;

namespace VehicleDummy.Repository.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly VehicleDummyDbContext _dbContext;

        public ShopRepository(VehicleDummyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Shop> CreateShopAsync(Shop shop)
        {
            _dbContext.Shops.Add(shop);
            _dbContext.SaveChangesAsync();
            return shop;
        }

        public async Task<List<Shop>> CreateShopListAsync(List<Shop> shops)
        {
            _dbContext.Shops.AddRange(shops);
            _dbContext.SaveChangesAsync();
            return shops;
        }

        public async Task DeleteShopAsync(Shop shop)
        {
            _dbContext.Shops.Remove(shop);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Shop>> GetAllShopAsync()
        {
            return await _dbContext.Shops.ToListAsync();
        }

        public async Task<Shop> GetShopByIdAsync(int shopId)
        {
            return await _dbContext.Shops.FirstOrDefaultAsync(shop => shop.ShopId == shopId);
        }

        public async Task<Shop> GetShopByNameAsync(string shopName)
        {
            return await _dbContext.Shops.FirstOrDefaultAsync(shop => shop.Name == shopName);
        }

        public async Task<Shop> UpdateShopAsync(Shop shop)
        {
            _dbContext.Shops.Update(shop);
            await _dbContext.SaveChangesAsync();
            return shop;
        }
    }
}
