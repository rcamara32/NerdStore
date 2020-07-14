using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Core.Bus;
using NerdStore.Sales.Application.Commands;

namespace NerdStore.WebApp.MVC.Controllers.Admin
{
    public class CartController : ControllerBase
    {
        private readonly IProductAppService _productAppService;
        private readonly IMediatorHandler _mediatorHandler;

        public CartController(
            IProductAppService productAppService,
            IMediatorHandler mediatorHandler)
        {
            _productAppService = productAppService;
            _mediatorHandler = mediatorHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(Guid id, int quantity)
        {
            var product = await _productAppService.GetById(id);
            if (product == null) return BadRequest();

            if (product.StockQuantity < quantity)
            {
                TempData["Error"] = "The product has insufficient stock.";
                return RedirectToAction("ProductDetails", "ShopWindow", new { id });
            }

            var command = new AddOrderItemCommand(ClientId, product.Id, product.Name, quantity, product.Price);
            var success = await _mediatorHandler.SendCommand(command);

            //if (success)
            //{
                    
            //}
            //else
            //{

            //}


            TempData["Error"] = "Product unavailable";
            return RedirectToAction("ProductDetails", "ShopWindow", new { id });

        }


    }
}