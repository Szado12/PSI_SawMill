using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreMicroService.Models;
using StoreMicroService.Services.Interfaces;
using StoreMicroService.Utils;
using StoreMicroService.ViewModels.Product;
using StoreMicroService.ViewModels.ProductType;
using StoreMicroService.ViewModels.WoodType;

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

    [HttpGet]
    [Route("{productId}")]
    public IActionResult GetProducts([FromRoute] int productId)
    {
      return _productService.GetProductById(productId).ToActionResult();
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


    [HttpPost]
    [Route("RemoveFromStore")]
    public IActionResult RemoveFromStore(List<ProductIdAndAmount> reserveItemList)
    {
      return _productService.ReserveProductInStore(reserveItemList).ToActionResult();
    }

    [HttpPost]
    [Route("AddToStore")]
    public IActionResult AddToStore(List<ProductIdAndAmount> reserveItemList)
    {
      return _productService.AddToStore(reserveItemList).ToActionResult();
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
    public IActionResult UpdateWoodType(WoodTypeModel woodType)
    {
      return _woodTypeService.UpdateWoodType(woodType).ToActionResult();
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
    public IActionResult UpdateProductType(ProductTypeModel productType)
    {
      return _productTypeService.UpdateProductType(productType).ToActionResult();
    }
  }
}
