using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Sales.Application.Commands;
using System;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class CartController : ControllerBase
    {
        private readonly IProductAppService _productAppService;
        private readonly IMediatorHandler _mediatorHandler;

        public CartController(INotificationHandler<DomainNotification> notifications,
                              IProductAppService productAppService,
                              IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _productAppService = productAppService;
            _mediatorHandler = mediatorHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("my-basket")]
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
            await _mediatorHandler.SendCommand(command).ConfigureAwait(true);

            if (IsOperationValid())
            {
                return RedirectToAction("Index");
            }
            
            TempData["Errors"] = GetErrorMessages();
            return RedirectToAction("ProductDetails", "ShopWindow", new { id });

        }


    }
}