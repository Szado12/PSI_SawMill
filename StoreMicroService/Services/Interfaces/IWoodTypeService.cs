using CSharpFunctionalExtensions;
using StoreMicroService.Models;
using StoreMicroService.ViewModels.WoodType;

namespace StoreMicroService.Services.Interfaces
{
  public interface IWoodTypeService
  {
    Result<string> AddWoodType(string name);
    Result<string> RemoveWoodType(int woodTypeId);
    Result<string> UpdateWoodType(WoodTypeModel woodType);
    Result<List<WoodTypeModel>> GetWoodTypes();
  }
}
