using CSharpFunctionalExtensions;
using StoreMicroService.Models;
using StoreMicroService.Services.Interfaces;
using StoreMicroService.ViewModels.WoodType;

namespace StoreMicroService.Services
{
  public class WoodTypeService : DefaultService,IWoodTypeService
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

    public Result<string> UpdateWoodType(WoodTypeModel woodType)
    {
      var woodTypeToChange = StoreContext.WoodTypes.FirstOrDefault(x => x.WoodTypeId == woodType.WoodTypeId);
      if (woodTypeToChange == null)
        return Result.Failure<string>($"Wood type with id:{woodType.WoodTypeId} doesn't exist");

      woodTypeToChange.Name = woodType.Name;
      StoreContext.SaveChanges();

      return Result.Success("Wood type updated");
    }

    public Result<List<WoodTypeModel>> GetWoodTypes()
    {
      return Result.Success(Mapper.Map<List<WoodType>,List<WoodTypeModel>>(StoreContext.WoodTypes.ToList()));
    }

    public WoodTypeService(StoreContext storeContext) : base(storeContext)
    {
    }
  }
}
