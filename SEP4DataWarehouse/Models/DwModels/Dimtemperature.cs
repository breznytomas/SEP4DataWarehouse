namespace SEP4DataWarehouse.Models.DwModels
{
    public partial class Dimtemperature
    {
        public Dimtemperature()
        {
            Factmeasurements = new HashSet<Factmeasurement>();
        }

        public int TId { get; set; }
        public int? Temperatureid { get; set; }
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
