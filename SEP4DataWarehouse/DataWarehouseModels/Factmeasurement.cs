using System;
using System.Collections.Generic;

namespace SEP4DataWarehouse.DataWarehouseModels
{
    public partial class Factmeasurement
    {
        public int Measurementid { get; set; }
        public int TId { get; set; }
        public int LId { get; set; }
        public int HId { get; set; }
        public int BId { get; set; }
        public int CdId { get; set; }
        public int DId { get; set; }
        public float? Temperaturevalue { get; set; }
        public float? Humidityvalue { get; set; }
        public float? Lightvalue { get; set; }
        public float? Carbondioxidevalue { get; set; }
        public TimeOnly? Timestamp { get; set; }

        public virtual Dimboard BIdNavigation { get; set; } = null!;
        public virtual Dimcarbondioxide Cd { get; set; } = null!;
        public virtual Dimdate DIdNavigation { get; set; } = null!;
        public virtual Dimhumidity HIdNavigation { get; set; } = null!;
        public virtual Dimlight LIdNavigation { get; set; } = null!;
        public virtual Dimtemperature TIdNavigation { get; set; } = null!;
    }
}
