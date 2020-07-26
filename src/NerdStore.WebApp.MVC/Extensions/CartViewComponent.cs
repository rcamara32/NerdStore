using Microsoft.AspNetCore.Mvc;
using NerdStore.Sales.Application.Queries;
using System;
using System.Threading.Tasks;


namespace NerdStore.WebApp.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IOrderQueries _pedidoQueries;

        // TODO: get user logged
        protected Guid ClientId = Guid.Parse("1bf7dc4d-c7fc-4d8b-b724-b7302d3bb63c");

        public CartViewComponent(IOrderQueries pedidoQueries)
        {
            _pedidoQueries = pedidoQueries;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var carrinho = await _pedidoQueries.GetCartByClientId(ClientId);
            var itens = carrinho?.Items.Count ?? 0;

            return View(itens);
        }
    }
}
