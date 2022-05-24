namespace SEP4DataWarehouse.Models.DwModels
{
    public partial class Dimuser
    {
        public int UId { get; set; }
        public string? Email { get; set; }
        public int? Validfrom { get; set; }
        public int? Validto { get; set; }
    }
}
