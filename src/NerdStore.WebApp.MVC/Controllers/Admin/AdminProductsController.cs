using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Dtos;
using NerdStore.Catalog.Application.Services;
using System;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.Controllers.Admin
{
    public class AdminProductsController : Controller
    {
        private readonly IProductAppService _productAppService;

        public AdminProductsController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("admin-products")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.GetAllProducts());
        }

        [Route("new-product")]
        public async Task<IActionResult> NewProduct()
        {
            return View(await GetAllGategoriesAsync(new ProductDto()));
        }

        [Route("new-product")]
        [HttpPost]
        public async Task<IActionResult> NewProduct(ProductDto product)
        {
            if (!ModelState.IsValid) return View(await GetAllGategoriesAsync(product));

            await _productAppService.AddProduct(product);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("edit-product")]
        public async Task<IActionResult> EditProduct(Guid id)
        {
            return View(await GetAllGategoriesAsync(await _productAppService.GetById(id)));
        }

        [HttpPost]
        [Route("edit-product")]
        public async Task<IActionResult> EditProduct(Guid id, ProductDto productDto)
        {
            var product = await _productAppService.GetById(id);
            productDto.StockQuantity = product.StockQuantity;

            ModelState.Remove("StockQuantity");
            if (!ModelState.IsValid) return View(await GetAllGategoriesAsync(productDto));

            await _productAppService.UpdateProduct(productDto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("product-update-stock")]
        public async Task<IActionResult> UpdateStock(Guid id)
        {
            return View("Stock", await _productAppService.GetById(id));
        }

        [HttpPost]
        [Route("product-update-stock")]
        public async Task<IActionResult> UpdateStock(Guid id, int quantity)
        {
            if (quantity > 0)
            {
                await _productAppService.StockReplacement(id, quantity);
            }
            else
            {
                await _productAppService.DebitStock(id, quantity);
            }

            return View("Index", await _productAppService.GetAllProducts());
        }

        private async Task<ProductDto> GetAllGategoriesAsync(ProductDto product)
        {
            product.Categories = await _productAppService.GetAllCategories();
            return product;
        }



    }
}