using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using System;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class ShopWindowController : Controller
    {
        private readonly IProductAppService _productAppService;

        public ShopWindowController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("")]
        [Route("shop")]
        public async Task<IActionResult> Index()
        {
            var products = await _productAppService.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        [Route("product-details/{id}")]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            return View(await _productAppService.GetById(id));
        }
    }
}