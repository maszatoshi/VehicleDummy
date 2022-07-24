using Microsoft.AspNetCore.Mvc;
using VehicleDummy.Models;
using VehicleDummy.Repository.Interfaces;

namespace VehicleDummy.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VehicleTestingController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IMeasurementPointRepository _measurementPointRepository;

        public VehicleTestingController(
                                    IMeasurementPointRepository measurementPointRepository,
                                    IMeasurementRepository measurementRepository,
                                    IShopRepository shopRepository,
                                    IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
            _measurementRepository = measurementRepository;
            _measurementPointRepository = measurementPointRepository;
            _shopRepository = shopRepository;
        }

        [HttpGet(Name = "GetMeasurementDataList/{count}")]
        [ActionName("GetMeasurementDataList")]
        public async Task<ActionResult<List<MeasurementResponse>>> GetMeasurementDataList(int count)
        {
            List<MeasurementResponse> result = new();
            List<Measurement> measurements = await _measurementRepository.GetTopMeasurementsAsnyc(count);
            foreach (var item in measurements)
            {
                MeasurementResponse response = (MeasurementResponse)item;
                Shop shop = await _shopRepository.GetShopByIdAsync(item.ShopId);
                Vehicle vehicle = await _vehicleRepository.GetVehicleByIdAsync(item.VehicleId);
                MeasurementPoint measurementPoint = await _measurementPointRepository.GetMeasurementPointByIdAsync(item.MeasurementPointId); 
                response.ShopName = shop.Name;
                response.VehicleJSN = vehicle.JSN;
                response.VehicleModelName = vehicle.VehicleModel;
                response.MeasurementPointName = measurementPoint.Name;
                result.Add(response);
            }
            return Ok(result);
        }

        [HttpPost(Name = "GetMeasurementDataListByDate")]
        [ActionName("GetMeasurementDataListByDate")]
        public async Task<ActionResult<List<MeasurementResponse>>> GetMeasurementDataListByDate(string date)
        {
            List<MeasurementResponse> result = new ();
            if (DateTime.TryParse(date, out DateTime dateTime))
            {
                List<Measurement> measurements = await _measurementRepository.GetAllMeasurementByDateAsync(dateTime);
                foreach (var item in measurements)
                {
                    MeasurementResponse response = (MeasurementResponse)item;
                    Shop shop = await _shopRepository.GetShopByIdAsync(item.ShopId);
                    Vehicle vehicle = await _vehicleRepository.GetVehicleByIdAsync(item.VehicleId);
                    MeasurementPoint measurementPoint = await _measurementPointRepository.GetMeasurementPointByIdAsync(item.MeasurementPointId);
                    response.ShopName = shop.Name;
                    response.VehicleJSN = vehicle.JSN;
                    response.VehicleModelName = vehicle.VehicleModel;
                    response.MeasurementPointName = measurementPoint.Name;
                    result.Add(response);
                }
                return Ok(result);
            }
            else
            {
                return BadRequest($"The given date is not correct.");
            }
            
        }

        [HttpPost(Name = "GetMeasurementsByJSN")]
        [ActionName("GetMeasurementsByJSN")]
        public async Task<ActionResult<List<MeasurementResponse>>> GetMeasurementsByJSN(string jsn)
        {
            List<MeasurementResponse> result = new();
            Vehicle vehicle = await _vehicleRepository.GetVehicleByJSNAsync(jsn);
            if (vehicle != null)
            {
                List<Measurement> measurements = await _measurementRepository.GetMeasurementListByVehicleId(vehicle.VehicleId);
                foreach (var item in measurements)
                {
                    MeasurementResponse response = (MeasurementResponse)item;
                    Shop shop = await _shopRepository.GetShopByIdAsync(item.ShopId);
                    MeasurementPoint measurementPoint = await _measurementPointRepository.GetMeasurementPointByIdAsync(item.MeasurementPointId);
                    response.ShopName = shop.Name;
                    response.VehicleJSN = vehicle.JSN;
                    response.VehicleModelName = vehicle.VehicleModel;
                    response.MeasurementPointName = measurementPoint.Name;
                    result.Add(response);
                }
                return Ok(result); 
            }
            else
            {
                return BadRequest($"Unable to find vehicle by JSN '{jsn}'.");
            }
        }

        [HttpPost(Name = "GetMeasurementsByShopName")]
        [ActionName("GetMeasurementsByShopName")]
        public async Task<ActionResult<List<MeasurementResponse>>> GetMeasurementsByShopName(string shopName)
        {
            List<MeasurementResponse> result = new();
            Shop shop = await _shopRepository.GetShopByNameAsync(shopName);
            if (shop != null)
            {
                List<Measurement> measurements = await _measurementRepository.GetMeasurementListByShopId(shop.ShopId);
                foreach (var item in measurements)
                {
                    MeasurementResponse response = (MeasurementResponse)item;
                    Vehicle vehicle = await _vehicleRepository.GetVehicleByIdAsync(item.VehicleId);
                    MeasurementPoint measurementPoint = await _measurementPointRepository.GetMeasurementPointByIdAsync(item.Id);
                    response.ShopName = shop.Name;
                    response.VehicleJSN = vehicle.JSN;
                    response.VehicleModelName = vehicle.VehicleModel;
                    response.MeasurementPointName = measurementPoint.Name;
                    result.Add(response);
                }
                return Ok(result); 
            }
            else
            {
                return BadRequest($"Unable to find shop by name '{shopName}'.");
            }
        }

        [HttpPost(Name = "GetMeasurementsByMeasurementPointName")]
        [ActionName("GetMeasurementsByMeasurementPointName")]
        public async Task<ActionResult<List<MeasurementResponse>>> GetMeasurementsByMeasurementPointName(string measurementPointName)
        {
            List<MeasurementResponse> result = new();
            MeasurementPoint measurementPoint = await _measurementPointRepository.GetMeasurementPointByNameAsync(measurementPointName);
            if (measurementPoint != null)
            {
                List<Measurement> measurements = await _measurementRepository.GetMeasurementListByMeasurementPointId(measurementPoint.MeasurementPointId);
                foreach (var item in measurements)
                {
                    MeasurementResponse response = (MeasurementResponse)item;
                    Vehicle vehicle = await _vehicleRepository.GetVehicleByIdAsync(item.VehicleId);
                    Shop shop = await _shopRepository.GetShopByIdAsync(item.ShopId);
                    response.ShopName = shop.Name;
                    response.VehicleJSN = vehicle.JSN;
                    response.VehicleModelName = vehicle.VehicleModel;
                    response.MeasurementPointName = measurementPoint.Name;
                    result.Add(response);
                }
                return Ok(result); 
            }
            else
            {
                return BadRequest($"Unable to find measurement point by name '{measurementPointName}'.");
            }
        }

        [HttpPost(Name = "AddMeasurement")]
        [ActionName("AddMeasurement")]
        public async Task<ActionResult> AddMeasurement(int vehicleId, int shopId, int measurementPointId,
                                                        decimal flush, decimal gap, string dateTime)
        {
            Measurement measurement = null;
            Vehicle vehicle = await _vehicleRepository.GetVehicleByIdAsync(vehicleId);
            Shop shop = await _shopRepository.GetShopByIdAsync(shopId);
            MeasurementPoint measurementPoint = await _measurementPointRepository.GetMeasurementPointByIdAsync(measurementPointId);
            bool dateok = DateTime.TryParse(dateTime, out DateTime time);
            if (vehicle != null && shop != null && measurementPoint != null && dateok)
            {
                measurement = new ()
                {
                    VehicleId = vehicleId,
                    ShopId = shopId,
                    MeasurementPointId = measurementPointId,
                    Gap = gap,
                    Flush = flush,
                    Date = time
                };
                await _measurementRepository.CreateMeasurement(measurement);
                return Ok();
            }
            else
            {
                string message = "";
                if (vehicle == null)
                    message += $"Unable to find vehicle by ID '{vehicleId}'.\n";
                if (shop == null)
                    message += $"Unable to find shop by ID '{shopId}'.\n";
                if (measurementPoint == null)
                    message += $"Unable to find measurement point by ID '{measurementPointId}'.\n";
                if (!dateok)
                    message += $"The given date is not correct.\n";
                return BadRequest(message);
            }
            
        }

        [HttpGet(Name = "GetMeasurementPoints")]
        [ActionName("GetMeasurementPoints")]
        public async Task<ActionResult<List<MeasurementPoint>>> GetMeasurementPoints()
            
        {
            List<MeasurementPoint> result = await _measurementPointRepository.GetAllMeasurementPointAsync();
            return Ok(result);
        }

        [HttpGet(Name = "GetMeasurementPointsByName/{name}")]
        [ActionName("GetMeasurementPointsByName")]
        public async Task<ActionResult<MeasurementPoint>> GetMeasurementPointByName(string name)

        {
            MeasurementPoint result = await _measurementPointRepository.GetMeasurementPointByNameAsync(name);
            if (result == null)
                return BadRequest($"Unable to find measurement point by name '{name}'.");
            else
                return Ok(result);
        }

        [HttpGet(Name = "GetVehicles")]
        [ActionName("GetVehicles")]
        public async Task<ActionResult<List<Vehicle>>> GetVehicles()
        {
            List<Vehicle> result = await _vehicleRepository.GetAllVehicleAsync();
            return Ok(result);
        }

        [HttpGet(Name = "GetVehicleByJSN/{jsn}")]
        [ActionName("GetVehicleByJSN")]
        public async Task<ActionResult<Vehicle>> GetVehicleByJSN(string jsn)
        {
            Vehicle result = await _vehicleRepository.GetVehicleByJSNAsync(jsn);
            if (result == null)
                return BadRequest($"Unable to find vehicle by JSN '{jsn}'.");
            else
                return Ok(result);
        }

        [HttpGet(Name = "GetShops")]
        [ActionName("GetShops")]
        public async Task<ActionResult<List<Shop>>> GetShops()
        {
            List<Shop> result = await _shopRepository.GetAllShopAsync();
            return Ok(result);
        }

        [HttpGet(Name = "GetShopByName/{shopName}")]
        [ActionName("GetShopByName")]
        public async Task<ActionResult<Shop>> GetShopByName(string shopName)
        {
            Shop result = await _shopRepository.GetShopByNameAsync(shopName);
            if (result == null)
                return BadRequest($"Unable to find shop by name '{shopName}'.");
            else
                return Ok(result);
        }

        

        

        
    }
}
