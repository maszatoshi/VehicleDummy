using VehicleDummy.Models;

namespace VehicleDummy.Repository.Interfaces
{
    public interface IShopRepository
    {
        Task<List<Shop>> GetAllShopAsync();
        Task<Shop> GetShopByIdAsync(int shopId);
        Task<Shop> GetShopByNameAsync(string shopName);
        Task<Shop> UpdateShopAsync(Shop shop);
        Task<Shop> CreateShopAsync(Shop shop);
        Task<List<Shop>> CreateShopListAsync(List<Shop> shops);
        Task DeleteShopAsync(Shop shop);
    }
}
