using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using VehicleDummy.Controllers;
using VehicleDummy.Models;
using VehicleDummy.Repository.Interfaces;

namespace VehicleDummyUnitTest
{
    [TestClass]
    public class VehicleTestingControllerUnitTest
    {
        private readonly Mock<IMeasurementPointRepository> _measurementPointRepositoryMock;
        private readonly Mock<IMeasurementRepository> _measurementRepositoryMock;
        private readonly Mock<IShopRepository> _shopRepositoryMock;
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;


        
        private List<Measurement> measurements;
        private Shop shop;
        private MeasurementPoint measurementPoint;
        private Vehicle vehicle;
        private const string text = "testString";
        private const string date = "2022.07.18";
        private const int number = 1;

        protected readonly VehicleTestingController controller;

        public VehicleTestingControllerUnitTest()
        {
            _measurementPointRepositoryMock = new Mock<IMeasurementPointRepository>();
            _measurementRepositoryMock = new Mock<IMeasurementRepository>();
            _shopRepositoryMock = new Mock<IShopRepository>();
            _vehicleRepositoryMock = new Mock<IVehicleRepository>();
            controller = new(
                measurementPointRepository: _measurementPointRepositoryMock.Object,
                measurementRepository: _measurementRepositoryMock.Object,
                vehicleRepository: _vehicleRepositoryMock.Object,
                shopRepository: _shopRepositoryMock.Object);
        }

        private void SetValues()
        {
            Measurement measurement = new() { ShopId = 1, VehicleId = 1, MeasurementPointId = 1 };
            measurements = new() { measurement, measurement };
            shop = new() { ShopId = 1, Name = "1" };
            measurementPoint = new() { MeasurementPointId = 1, Name = "1" };
            vehicle = new() { VehicleId = 1, JSN = "1", VehicleModel = "1" };
        }

        [TestMethod]
        public async Task GetMeasurementDataList_ReturnsOk()
        {
            SetValues();
            Random random = new Random();
            int count = random.Next(1, 1000);
            _measurementRepositoryMock.Setup(x => x.GetTopMeasurementsAsnyc(It.IsAny<int>())).ReturnsAsync(measurements);
            _measurementPointRepositoryMock.Setup(x => x.GetMeasurementPointByIdAsync(It.IsAny<int>())).ReturnsAsync(measurementPoint);
            _shopRepositoryMock.Setup(x => x.GetShopByIdAsync(It.IsAny<int>())).ReturnsAsync(shop);
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByIdAsync(It.IsAny<int>())).ReturnsAsync(vehicle);
            var response = (await controller.GetMeasurementDataList(count)).Result as OkObjectResult;

            Assert.AreEqual(response.StatusCode, 200);
        }



        [TestMethod]
        public async Task GetMeasurementsByJSN_ReturnsOk()
        {
            SetValues();
            _measurementRepositoryMock.Setup(x => x.GetMeasurementListByVehicleId(It.IsAny<int>())).ReturnsAsync(measurements);
            _measurementPointRepositoryMock.Setup(x => x.GetMeasurementPointByIdAsync(It.IsAny<int>())).ReturnsAsync(measurementPoint);
            _shopRepositoryMock.Setup(x => x.GetShopByIdAsync(It.IsAny<int>())).ReturnsAsync(shop);
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByJSNAsync(It.IsAny<string>())).ReturnsAsync(vehicle);
            var response = (await controller.GetMeasurementsByJSN(text)).Result as OkObjectResult;

            Assert.AreEqual(response.StatusCode, 200);
        }

        [TestMethod]
        public async Task GetMeasurementsByJSN_ReturnsBadRequest()
        {
            SetValues();
            _measurementRepositoryMock.Setup(x => x.GetMeasurementListByVehicleId(It.IsAny<int>())).ReturnsAsync(measurements);
            _measurementPointRepositoryMock.Setup(x => x.GetMeasurementPointByIdAsync(It.IsAny<int>())).ReturnsAsync(measurementPoint);
            _shopRepositoryMock.Setup(x => x.GetShopByIdAsync(It.IsAny<int>())).ReturnsAsync(shop);
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByJSNAsync(It.IsAny<string>()));
            var response = (await controller.GetMeasurementsByJSN(text)).Result as ObjectResult;

            Assert.AreEqual(response.StatusCode, 400);
        }

        [TestMethod]
        public async Task GetMeasurementsByShopName_ReturnsOk()
        {
            SetValues();
            _measurementRepositoryMock.Setup(x => x.GetMeasurementListByShopId(It.IsAny<int>())).ReturnsAsync(measurements);
            _measurementPointRepositoryMock.Setup(x => x.GetMeasurementPointByIdAsync(It.IsAny<int>())).ReturnsAsync(measurementPoint);
            _shopRepositoryMock.Setup(x => x.GetShopByNameAsync(It.IsAny<string>())).ReturnsAsync(shop);
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByIdAsync(It.IsAny<int>())).ReturnsAsync(vehicle);
            var response = (await controller.GetMeasurementsByShopName(text)).Result as OkObjectResult;

            Assert.AreEqual(response.StatusCode, 200);
        }

        [TestMethod]
        public async Task GetMeasurementsByShopName_ReturnsBadRequest()
        {
            SetValues();
            _measurementRepositoryMock.Setup(x => x.GetMeasurementListByShopId(It.IsAny<int>())).ReturnsAsync(measurements);
            _measurementPointRepositoryMock.Setup(x => x.GetMeasurementPointByIdAsync(It.IsAny<int>())).ReturnsAsync(measurementPoint);
            _shopRepositoryMock.Setup(x => x.GetShopByNameAsync(It.IsAny<string>()));
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByIdAsync(It.IsAny<int>())).ReturnsAsync(vehicle);
            var response = (await controller.GetMeasurementsByShopName(text)).Result as ObjectResult;

            Assert.AreEqual(response.StatusCode, 400);
        }

        [TestMethod]
        public async Task GetMeasurementDataListByDate_ReturnsOk()
        {
            SetValues();
            _measurementRepositoryMock.Setup(x => x.GetAllMeasurementByDateAsync(It.IsAny<DateTime>())).ReturnsAsync(measurements);
            _measurementPointRepositoryMock.Setup(x => x.GetMeasurementPointByIdAsync(It.IsAny<int>())).ReturnsAsync(measurementPoint);
            _shopRepositoryMock.Setup(x => x.GetShopByIdAsync(It.IsAny<int>())).ReturnsAsync(shop);
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByIdAsync(It.IsAny<int>())).ReturnsAsync(vehicle);
            var response = (await controller.GetMeasurementDataListByDate(date)).Result as OkObjectResult;

            Assert.AreEqual(response.StatusCode, 200);
        }

        public async Task GetMeasurementDataListByDate_ReturnsBadRequest()
        {
            SetValues();
            _measurementRepositoryMock.Setup(x => x.GetAllMeasurementByDateAsync(It.IsAny<DateTime>())).ReturnsAsync(measurements);
            _measurementPointRepositoryMock.Setup(x => x.GetMeasurementPointByIdAsync(It.IsAny<int>())).ReturnsAsync(measurementPoint);
            _shopRepositoryMock.Setup(x => x.GetShopByIdAsync(It.IsAny<int>())).ReturnsAsync(shop);
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByIdAsync(It.IsAny<int>())).ReturnsAsync(vehicle);
            var response = (await controller.GetMeasurementDataListByDate(text)).Result as ObjectResult;

            Assert.AreEqual(response.StatusCode, 400);
        }

        [TestMethod]
        public async Task GetMeasurementsByMeasurementPointName_ReturnsOk()
        {
            SetValues();
            _measurementRepositoryMock.Setup(x => x.GetMeasurementListByMeasurementPointId(It.IsAny<int>())).ReturnsAsync(measurements);
            _measurementPointRepositoryMock.Setup(x => x.GetMeasurementPointByNameAsync(It.IsAny<string>())).ReturnsAsync(measurementPoint);
            _shopRepositoryMock.Setup(x => x.GetShopByIdAsync(It.IsAny<int>())).ReturnsAsync(shop);
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByIdAsync(It.IsAny<int>())).ReturnsAsync(vehicle);
            var response = (await controller.GetMeasurementsByMeasurementPointName(text)).Result as OkObjectResult;

            Assert.AreEqual(response.StatusCode, 200);
        }

        [TestMethod]
        public async Task GetMeasurementsByMeasurementPointName_ReturnsBadRequest()
        {
            SetValues();
            _measurementRepositoryMock.Setup(x => x.GetMeasurementListByMeasurementPointId(It.IsAny<int>())).ReturnsAsync(measurements);
            _measurementPointRepositoryMock.Setup(x => x.GetMeasurementPointByNameAsync(It.IsAny<string>()));
            _shopRepositoryMock.Setup(x => x.GetShopByIdAsync(It.IsAny<int>())).ReturnsAsync(shop);
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByIdAsync(It.IsAny<int>())).ReturnsAsync(vehicle);
            var response = (await controller.GetMeasurementsByMeasurementPointName(text)).Result as ObjectResult;

            Assert.AreEqual(response.StatusCode, 400);
        }

        [TestMethod]
        public async Task GetMeasurementPoints_ReturnsOk()
        {
            SetValues();
            _measurementPointRepositoryMock.Setup(x => x.GetAllMeasurementPointAsync()).ReturnsAsync(new List<MeasurementPoint>());
            var response = (await controller.GetMeasurementPoints()).Result as OkObjectResult;

            Assert.AreEqual(response.StatusCode, 200);
        }

        [TestMethod]
        public async Task GetMeasurementPointByName_ReturnsOk()
        {
            SetValues();
            _measurementPointRepositoryMock.Setup(x => x.GetMeasurementPointByNameAsync(It.IsAny<string>())).ReturnsAsync(measurementPoint);
            var response = (await controller.GetMeasurementPointByName(text)).Result as OkObjectResult;

            Assert.AreEqual(response.StatusCode, 200);
        }

        [TestMethod]
        public async Task GetMeasurementPointByName_ReturnsBadRequest()
        {
            SetValues();
            _measurementPointRepositoryMock.Setup(x => x.GetMeasurementPointByNameAsync(It.IsAny<string>()));
            var response = (await controller.GetMeasurementPointByName(text)).Result as ObjectResult;

            Assert.AreEqual(response.StatusCode, 400);
        }

        [TestMethod]
        public async Task GetVehicles_ReturnsOk()
        {
            SetValues();
            _vehicleRepositoryMock.Setup(x => x.GetAllVehicleAsync()).ReturnsAsync(new List<Vehicle>());
            var response = (await controller.GetVehicles()).Result as OkObjectResult;

            Assert.AreEqual(response.StatusCode, 200);
        }

        [TestMethod]
        public async Task GetVehicleByJSN_ReturnsOk()
        {
            SetValues();
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByJSNAsync(It.IsAny<string>())).ReturnsAsync(vehicle);
            var response = (await controller.GetVehicleByJSN(text)).Result as OkObjectResult;

            Assert.AreEqual(response.StatusCode, 200);
        }

        [TestMethod]
        public async Task GetVehicleByJSN_ReturnsBadRequest()
        {
            SetValues();
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByJSNAsync(It.IsAny<string>()));
            var response = (await controller.GetVehicleByJSN(text)).Result as ObjectResult;

            Assert.AreEqual(response.StatusCode, 400);
        }

        [TestMethod]
        public async Task GetShops_ReturnsOk()
        {
            SetValues();
            _shopRepositoryMock.Setup(x => x.GetAllShopAsync()).ReturnsAsync(new List<Shop>());
            var response = (await controller.GetShops()).Result as OkObjectResult;

            Assert.AreEqual(response.StatusCode, 200);
        }

        [TestMethod]
        public async Task GetShopByName_ReturnsOk()
        {
            SetValues();
            _shopRepositoryMock.Setup(x => x.GetShopByNameAsync(It.IsAny<string>())).ReturnsAsync(shop);
            var response = (await controller.GetShopByName(text)).Result as OkObjectResult;

            Assert.AreEqual(response.StatusCode, 200);
        }

        [TestMethod]
        public async Task GetShopByName_ReturnsBadRequest()
        {
            SetValues();
            _shopRepositoryMock.Setup(x => x.GetShopByNameAsync(It.IsAny<string>()));
            var response = (await controller.GetShopByName(text)).Result as ObjectResult;

            Assert.AreEqual(response.StatusCode, 400);
        }





        [TestMethod]
        public async Task GetMeasurementDataList_ReturnsCorrectNumerOfItems()
        {
            SetValues();
            _measurementRepositoryMock.Setup(x => x.GetTopMeasurementsAsnyc(It.IsAny<int>())).ReturnsAsync(measurements);
            _measurementPointRepositoryMock.Setup(x => x.GetMeasurementPointByIdAsync(It.IsAny<int>())).ReturnsAsync(measurementPoint);
            _shopRepositoryMock.Setup(x => x.GetShopByIdAsync(It.IsAny<int>())).ReturnsAsync(shop);
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByIdAsync(It.IsAny<int>())).ReturnsAsync(vehicle);

            var result = (await controller.GetMeasurementDataList(measurements.Count)).Result as OkObjectResult;
            var list = result.Value as List<MeasurementResponse>;
            Assert.AreEqual(list.Count, 2);
        }

        [TestMethod]
        public async Task AddMeasurement_ReturnsOk()
        {
            SetValues();
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByIdAsync(It.IsAny<int>())).ReturnsAsync(vehicle);
            _shopRepositoryMock.Setup(x => x.GetShopByIdAsync(It.IsAny<int>())).ReturnsAsync(shop);
            _measurementPointRepositoryMock.Setup(x => x.GetMeasurementPointByIdAsync(It.IsAny<int>())).ReturnsAsync(measurementPoint);
            _measurementRepositoryMock.Setup(x => x.CreateMeasurement(It.IsAny<Measurement>())).ReturnsAsync(new Measurement());
            var result = (await controller.AddMeasurement(number, number, number, 1, 1, date)) as OkResult;

            Assert.AreEqual(result.StatusCode, 200);
        }

        [TestMethod]
        public async Task AddMeasurement_BadRequest()
        {
            SetValues();
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByIdAsync(It.IsAny<int>()));
            _shopRepositoryMock.Setup(x => x.GetShopByIdAsync(It.IsAny<int>()));
            _measurementPointRepositoryMock.Setup(x => x.GetMeasurementPointByIdAsync(It.IsAny<int>()));
            _measurementRepositoryMock.Setup(x => x.CreateMeasurement(It.IsAny<Measurement>()));
            var result = (await controller.AddMeasurement(number, number, number, 1, 1, date)) as BadRequestObjectResult;

            Assert.AreEqual(result.StatusCode, 400);
        }
    }
}