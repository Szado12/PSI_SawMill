using CSharpFunctionalExtensions;
using StoreMicroService.Models;
using StoreMicroService.Services.Interfaces;

namespace StoreMicroService.Services
{
  public class WoodTypeService : DeafultService,IWoodTypeService
  {
    public Result<string> AddWoodType(string name)
    {
      bool exist = StoreContext.WoodTypes.Count(x => x.Name.ToLower() == name.ToLower()) > 0;
      if (exist)
        return Result.Failure<string>("Wood type with this name already exist");

      StoreContext.WoodTypes.Add(new WoodType() {Name = name});
      StoreContext.SaveChanges();
      return Result.Success($"Wood type {name} added");
    }

    public Result<string> RemoveWoodType(int woodTypeId)
    {
      throw new NotImplementedException();
    }

    public Result<string> UpdateWoodType(int woodTypeId, string updatedName)
    {
      var woodTypeToChange = StoreContext.WoodTypes.FirstOrDefault(x => x.WoodTypeId == woodTypeId);
      if (woodTypeToChange == null)
        return Result.Failure<string>($"Wood type with id:{woodTypeId} doesn't exist");

      woodTypeToChange.Name = updatedName;
      StoreContext.SaveChanges();

      return Result.Success("Wood type updated");
    }

    public Result<List<WoodType>> GetWoodTypes()
    {
      return Result.Success(StoreContext.WoodTypes.ToList());
    }
  }
}
