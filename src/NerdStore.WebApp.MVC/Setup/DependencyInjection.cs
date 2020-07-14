using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data;
using NerdStore.Catalog.Data.Repositories;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Interface.Repostory;
using NerdStore.Catalog.Domain.Interface.Service;
using NerdStore.Catalog.Domain.Services;
using NerdStore.Core.Bus;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Data.Repository;
using NerdStore.Sales.Domain.Interface;

namespace NerdStore.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();
                 
            // Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();       

            services.AddScoped<INotificationHandler<LowStockEvent>, ProductEventHandler>();


            //Sales
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, OrderCommandHandler>();

        }
    }
}
