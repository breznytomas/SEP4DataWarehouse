namespace SEP4DataWarehouse.Models.DwModels
{
    public partial class Dimboard
    {
        public Dimboard()
        {
            Factmeasurements = new HashSet<Factmeasurement>();
        }

        public int BId { get; set; }
        public string? BoardId { get; set; }
        public string? Name { get; set; }
        public int? Validfrom { get; set; }
        public int? Validto { get; set; }

        public virtual ICollection<Factmeasurement> Factmeasurements { get; set; }
    }
}
