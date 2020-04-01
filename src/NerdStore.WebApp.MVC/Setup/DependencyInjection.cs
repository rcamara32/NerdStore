using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data;
using NerdStore.Catalog.Data.Repositories;
using NerdStore.Catalog.Domain.Interface.Repostory;
using NerdStore.Catalog.Domain.Interface.Service;
using NerdStore.Catalog.Domain.Services;
using NerdStore.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            //// Notifications
            //services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Event Sourcing
            //services.AddSingleton<IEventStoreService, LowStockEvent>();
            //services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();

            // Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();

            //services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();
            //services.AddScoped<INotificationHandler<PedidoIniciadoEvent>, ProdutoEventHandler>();
            //services.AddScoped<INotificationHandler<PedidoProcessamentoCanceladoEvent>, ProdutoEventHandler>();


        }
    }
}
