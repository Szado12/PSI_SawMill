using CSharpFunctionalExtensions;
using StoreMicroService.Models;
using StoreMicroService.ViewModels.WoodType;

namespace StoreMicroService.Services.Interfaces
{
  public interface IWoodTypeService
  {
    Result<int> AddWoodType(string name);
    Result<int> RemoveWoodType(int woodTypeId);
    Result<int> UpdateWoodType(WoodTypeModel woodType);
    Result<List<WoodTypeModel>> GetWoodTypes();
  }
}
