namespace SEP4DataWarehouse.Models.DwModels
{
    public partial class Dimdate
    {
        public Dimdate()
        {
            Factmeasurements = new HashSet<Factmeasurement>();
        }

        public int DId { get; set; }
        public DateOnly? Date { get; set; }
        public int? Day { get; set; }
        public string? DayName { get; set; }
        public int? Week { get; set; }
        public int? Month { get; set; }
        public string? Monthname { get; set; }
        public int? Quarter { get; set; }
        public int? Year { get; set; }

        public virtual ICollection<Factmeasurement> Factmeasurements { get; set; }
    }
}
