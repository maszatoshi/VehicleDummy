namespace VehicleDummy.Models
{
    public class MeasurementResponse
    {
        public string MeasurementPointName { get; set; }
        public string VehicleJSN { get; set; }
        public decimal Gap { get; set; }
        public decimal Flush { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string ShopName { get; set; }
        public string VehicleModelName { get; set; }

        public static implicit operator MeasurementResponse(Measurement measurement)
        {
            return new MeasurementResponse()
            {
                Date = measurement.Date.ToShortDateString(),
                Time = measurement.Date.ToShortTimeString(),
                Gap =  measurement.Gap,
                Flush = measurement.Flush
            };
        }
    }
    
}
