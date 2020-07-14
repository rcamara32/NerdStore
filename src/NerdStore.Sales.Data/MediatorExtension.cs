using System.Linq;
using System.Threading.Tasks;
using NerdStore.Core.Bus;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Sales.Data
{
    public static class MediatorExtension
    {
        public static async Task PublishEvent(this IMediatorHandler mediator, SalesContext ctx)
        {
            
            //var domainEntities = ctx.ChangeTracker
            //    .Entries<Entity>()
            //    .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            //var domainEvents = domainEntities
            //    .SelectMany(x => x.Entity.Notifications)
            //    .ToList();

            //domainEntities.ToList()
            //    .ForEach(entity => entity.Entity.ClearEvents());

            //var tasks = domainEvents
            //    .Select(async (domainEvent) => {
            //        await mediator.PublishEvent(domainEvent);
            //    });

            //await Task.WhenAll(tasks);
        }
    }
}
