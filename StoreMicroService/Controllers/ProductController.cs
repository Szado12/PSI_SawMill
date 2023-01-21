using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreMicroService.Models;
using StoreMicroService.Services.Interfaces;
using StoreMicroService.Utils;
using StoreMicroService.ViewModels.Product;

namespace StoreMicroService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    private IProductService _productService;
    private IProductTypeService _productTypeService;
    private IWoodTypeService _woodTypeService;

    public ProductController(IProductService productService, IProductTypeService productTypeService, IWoodTypeService woodTypeService)
    {
      _productService = productService;
      _productTypeService = productTypeService;
      _woodTypeService = woodTypeService;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
      return _productService.GetAllProducts().ToActionResult();
    }

    [HttpPost]
    public IActionResult AddProduct(AddProductViewModel product)
    {
      return _productService.AddProduct(product).ToActionResult();
    }

    [HttpPut]
    public IActionResult UpdateProduct(UpdateProductViewModel product)
    {
      return _productService.UpdateProduct(product).ToActionResult();
    }

    [HttpGet]
    [Route("WoodType")]
    public IActionResult GetWoodTypes()
    {
      return _woodTypeService.GetWoodTypes().ToActionResult();
    }

    [HttpPost]
    [Route("WoodType")]
    public IActionResult AddWoodType(string woodTypeName)
    {
      return _woodTypeService.AddWoodType(woodTypeName).ToActionResult();
    }

    [HttpPut]
    [Route("WoodType")]
    public IActionResult UpdateWoodType(WoodType woodType)
    {
      return _woodTypeService.UpdateWoodType(woodType.WoodTypeId,woodType.Name).ToActionResult();
    }

    [HttpGet]
    [Route("ProductType")]
    public IActionResult GetProductTypes()
    {
      return _productTypeService.GetProductTypes().ToActionResult();
    }

    [HttpPost]
    [Route("ProductType")]
    public IActionResult AddProductType(string woodTypeName)
    {
      return _productTypeService.AddProductType(woodTypeName).ToActionResult();
    }

    [HttpPut]
    [Route("ProductType")]
    public IActionResult UpdateProductType(ProductType productType)
    {
      return _productTypeService.UpdateProductType(productType.ProductTypeId, productType.Name).ToActionResult();
    }

  }
}
