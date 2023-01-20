using CSharpFunctionalExtensions;
using StoreMicroService.Models;

namespace StoreMicroService.Services.Interfaces
{
  public interface IWoodTypeService
  {
    Result<string> AddWoodType(string name);
    Result<string> RemoveWoodType(int woodTypeId);
    Result<string> UpdateWoodType(int woodTypeId, string updatedName);
    Result<List<WoodType>> GetWoodTypes();
  }
}
