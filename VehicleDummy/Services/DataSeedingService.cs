using System.Text;
using VehicleDummy.Models;

namespace VehicleDummy.Services
{
    public class DataSeedingService
    {
        private List<Vehicle> _vehicleList;
        private List<Shop> _shopList;
        private List<MeasurementPoint> _measurementPointList;
        private List<Measurement> _measurements;

        private Random random;

        public DataSeedingService()
        {
            _vehicleList = new List<Vehicle>();
            _shopList = new List<Shop>();
            _measurementPointList = new List<MeasurementPoint>();
            _measurements = new List<Measurement>();
            random = new();
        }

        /// <summary>
        /// Csak egyszer hozza létre a listát az adatbázis feltötéséhez.
        /// Erre azért van szükség mert a GenerateMeasurement() ebből veszi ki rendom az adatokat.
        /// </summary>
        /// <param name="numberOfEntities"></param>
        /// <returns></returns>
        public List<Vehicle> GenerateVehicleList(int numberOfEntities = 100)
        {
            if (!_vehicleList.Any())
            {
                for (int i = 1; i <= numberOfEntities; i++)
                {
                    _vehicleList.Add(new Vehicle()
                    {
                        VehicleId = i,
                        JSN = GenerateRandomNumberString(14),
                        VehicleModel = GenerateVehicleModel()
                    });
                } 
            }

            return _vehicleList;
        }

        /// <summary>
        /// Csak egyszer hozza létre a listát az adatbázis feltötéséhez.
        /// Erre azért van szükség mert a GenerateMeasurement() ebből veszi ki rendom az adatokat.
        /// </summary>
        /// <param name="numberOfEntities"></param>
        /// <returns></returns>
        public List<Shop> GenerateShopList(int numberOfEntities = 10)
        {
            if (!_shopList.Any())
            {
                for (int i = 1; i <= numberOfEntities; i++)
                {
                    _shopList.Add(new Shop()
                    {
                        ShopId = i,
                        Name = $@"AUDI Q3\Spalt & Bündigkeit\MZ_Karobau_{i}"
                    });
                } 
            }
            return _shopList;
        }

        /// <summary>
        /// Csak egyszer hozza létre a listát az adatbázis feltötéséhez.
        /// Erre azért van szükség mert a GenerateMeasurement() ebből veszi ki rendom az adatokat.
        /// </summary>
        /// <param name="numberOfEntities"></param>
        /// <returns></returns>
        public List<MeasurementPoint> GenerateMeasurementPointList(int numberOfEntities = 100)
        {
            if (!_measurementPointList.Any())
            {
                for (int i = 1; i <= numberOfEntities; i++)
                {
                    _measurementPointList.Add(new MeasurementPoint()
                    {
                        MeasurementPointId = i,
                        Name = i % 2 != 0 ? GenerateMeasurementPointName() : GenerateMeasurementPointName(_measurementPointList.First(x => x.MeasurementPointId == (i - 1)).Name)
                    });
                }
            }
            return _measurementPointList;
        }

        /// <summary>
        /// Memóriakezelés miatt (mivel 100k rekordot kell létrehozni), minden egyes meghívott alkalommal 
        /// tisztítja a listát és újakat generál.
        /// </summary>
        /// <param name="numberOfEntities"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Measurement> GenerateMeasurementList(int numberOfEntities = 1000, int startId = 1)
        {
            _measurements.Clear();
            for (int i = startId; i <= (startId + numberOfEntities); i++)
            {
                _measurements.Add(new Measurement()
                {
                    Id = i,
                    VehicleId = _vehicleList[random.Next(0, _vehicleList.Count - 1)].VehicleId,
                    ShopId = _shopList[random.Next(0, _shopList.Count - 1)].ShopId,
                    MeasurementPointId = _measurementPointList[random.Next(0, _measurementPointList.Count - 1)].MeasurementPointId,
                    Date = RandomDay(),
                    Flush = NextDecimal(),
                    Gap = NextDecimal()
                }) ;
            }

            return _measurements;

        }

        private string GenerateRandomNumberString(int length)
        {
            StringBuilder stringBuilder = new();
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(random.Next(0, 9));
            }
            return stringBuilder.ToString();
        }

        private string GenerateRandomAlphabetString(int length, bool isUpperCase)
        {
            StringBuilder stringBuilder = new();

            char offset = 'a';
            int letterOffSet = 26;

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(GetRandomChar());
            }
            return isUpperCase ? stringBuilder.ToString().ToUpper() : stringBuilder.ToString();
        }

        private char GetRandomChar()
        {
            string chars = "abcdefghijklmnopqrstuvwxyz";
            int num = random.Next(0, chars.Length);
            return chars[num];
        }

        private string GenerateVehicleModel()
        {
            StringBuilder stringBuilder = new ();


            stringBuilder.Append(GenerateRandomAlphabetString(1, true));
            stringBuilder.Append(GenerateRandomNumberString(1));
            stringBuilder.Append('/');
            stringBuilder.Append(GenerateRandomAlphabetString(2, true));
            stringBuilder.Append(GenerateRandomNumberString(3));

            return stringBuilder.ToString().ToUpper();
        }

        private string GenerateMeasurementPointName(string firstName = "")
        {
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                firstName = firstName.Substring(0, 10) + "L";
                return firstName;
            }
            else
            {
                StringBuilder stringBuilder = new();
                stringBuilder.Append(GenerateRandomNumberString(3));
                stringBuilder.Append('_');
                stringBuilder.Append(GenerateRandomAlphabetString(1, true));
                stringBuilder.Append(GenerateRandomNumberString(3));
                stringBuilder.Append(GenerateRandomAlphabetString(1, false));
                stringBuilder.Append('_');
                stringBuilder.Append('R');

                return stringBuilder.ToString();
            }
        }

        private DateTime RandomDay()
        {
            DateTime start = DateTime.Now - TimeSpan.FromDays(10);
            DateTime end = DateTime.Now;

            TimeSpan timeSpan = end - start;

            byte[] bytes = new byte[8];
            random.NextBytes(bytes);

            long int64 = Math.Abs(BitConverter.ToInt64(bytes, 0)) % timeSpan.Ticks;

            TimeSpan newSpan = new TimeSpan(int64);

            DateTime result = start + newSpan;

            return result;
        }

        public decimal NextDecimal()
        {
            double result = Math.Round((random.NextDouble() * Math.Abs(5 - 0) + 0), 2);
            return Convert.ToDecimal(result);

        }

    }
}
