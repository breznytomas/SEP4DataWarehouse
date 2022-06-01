using SEP4DataWarehouse.DTO.DbDTO;

namespace SEP4DataWarehouse.BusinessLogic;

public interface ICheckForValuesService
{
    Task<int> CheckForDeviations(ReadingDto reading);
}