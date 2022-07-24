using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleDummy.Models
{
    /// <summary>
    /// Autó
    /// </summary>
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        /// <summary>
        /// Autó egyedi azonosítója
        /// </summary>
        public string JSN { get; set; }
        /// <summary>
        /// Az autó modell típusa (Alaktrész)
        /// </summary>
        public string VehicleModel { get; set; }
    }

    /// <summary>
    /// Mérési hely
    /// </summary>
    public class Shop
    {
        [Key]
        public int ShopId { get; set; }
        /// <summary>
        /// Gyár egység (Állomás)
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Mérési pont
    /// </summary>
    public class MeasurementPoint
    {
        [Key]
        public int MeasurementPointId { get; set; }
        /// <summary>
        /// Mérési pont neve
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Mérési eredmény (rekord)
    /// </summary>
    public class Measurement
    {
        [Key]
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int ShopId { get; set; }
        public int MeasurementPointId { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal Gap { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal Flush { get; set; }
    }
}
