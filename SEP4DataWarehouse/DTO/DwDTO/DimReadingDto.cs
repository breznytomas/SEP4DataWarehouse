namespace SEP4DataWarehouse.DTO.DwDTO; 

public class DimReadingDto {
    
    public long ID { get; set; }
    public DateTime MeasureDate { get; set; }
    public float Value { get; set; }
    public string TriggeredFrom { get; set; }
    public float ExceededBy { get; set; }
    
}