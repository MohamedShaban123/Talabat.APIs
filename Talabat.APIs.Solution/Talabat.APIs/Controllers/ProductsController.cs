using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Cryptography.Xml;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Products_Specs;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Products_Specs;

namespace Talabat.APIs.Controllers
{
   
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<ProductBrand> _brandsRepo;
        //private readonly IGenericRepository<ProductCategory> _categoriesRepo;

        public IMapper _mapper { get; }

        public ProductsController(
             IMapper mapper , 
             IProductService productService
            //IGenericRepository<Product> productRepo,
            //IGenericRepository<ProductBrand> brandsRepo,
            //IGenericRepository<ProductCategory> categoriesRepo
            )
        {
            // _productRepo = productRepo;
            _mapper = mapper;
            _productService = productService;
            //  _brandsRepo = brandsRepo;
            //_categoriesRepo = categoriesRepo;
        }


        //// /api/Products
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        //{
        //    var products= await _productRepo.GetAllAsync();
        //   return Ok(products);
        //}

        ////  /api/Products/1
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Product>> GetProduct(int id)
        //{
        //    var product = await _productRepo.GetAsync(id);
        //    if (product == null)
        //        return NotFound(new {Message="Product Not Found " , StatusCode=404});  // StatusCode  404
        //    return Ok(product);    // StatusCode  200
        //}


        /* Apply Specification Design Pattern*/


        // /api/Products
        
        [HttpGet]
        //[Authorize/*(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)*/]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var products = await _productService.GetProductsAsync(specParams);
            var count = await _productService.GetCountAsync(specParams);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var countSpec = new ProductWithFilterationForCountSpecifications(specParams);
            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex,specParams.PageSize , count, data));
        }

        //  /api/Products/1
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
                return NotFound(new ApiResponse(404));  // StatusCode  404
            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));    // StatusCode  200
        }













        [HttpGet("brands")] //Get : /api/products/brands

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _productService.GetBrandsAsync();
            if (brands is null)
                return NotFound( new ApiResponse(404));
            return Ok(brands);
        }

        [HttpGet("categories")] //Get : api/Products/categories
        

        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
        {
            var categories =  await _productService.GetCategoriesAsync();
            if (categories is null)
                return NotFound(new ApiResponse(404));
            return Ok(categories);
        }



    }
}
