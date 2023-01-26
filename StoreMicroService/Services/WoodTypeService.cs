using CSharpFunctionalExtensions;
using StoreMicroService.Models;
using StoreMicroService.Services.Interfaces;
using StoreMicroService.ViewModels.WoodType;

namespace StoreMicroService.Services
{
  public class WoodTypeService : DefaultService,IWoodTypeService
  {
    public Result<int> AddWoodType(string name)
    {
      bool exist = StoreContext.WoodTypes.Count(x => x.Name.ToLower() == name.ToLower()) > 0;
      if (exist)
        return Result.Failure<int>("Wood type with this name already exist");

      var woodType = new WoodType() {Name = name};
      StoreContext.WoodTypes.Add(woodType);
      StoreContext.SaveChanges();
      return Result.Success(woodType.WoodTypeId);
    }

    public Result<int> RemoveWoodType(int woodTypeId)
    {
      throw new NotImplementedException();
    }

    public Result<int> UpdateWoodType(WoodTypeModel woodType)
    {
      var woodTypeToChange = StoreContext.WoodTypes.FirstOrDefault(x => x.WoodTypeId == woodType.WoodTypeId);
      if (woodTypeToChange == null)
        return Result.Failure<int>($"Wood type with id:{woodType.WoodTypeId} doesn't exist");

      woodTypeToChange.Name = woodType.Name;
      StoreContext.SaveChanges();

      return Result.Success(woodType.WoodTypeId);
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
