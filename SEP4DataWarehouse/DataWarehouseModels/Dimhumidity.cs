using System;
using System.Collections.Generic;

namespace SEP4DataWarehouse.DataWarehouseModels
{
    public partial class Dimhumidity
    {
        public Dimhumidity()
        {
            Factmeasurements = new HashSet<Factmeasurement>();
        }

        public int HId { get; set; }
        public int? Humidityid { get; set; }
        public float? Upperlimit { get; set; }
        public float? Lowerlimit { get; set; }
        public int? MeasureDate { get; set; }
        public int? Validfrom { get; set; }
        public int? Validto { get; set; }
        public string? Wastriggered { get; set; }
        public string? Istop { get; set; }

        public virtual ICollection<Factmeasurement> Factmeasurements { get; set; }
    }
}
