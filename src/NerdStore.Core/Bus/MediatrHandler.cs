using MediatR;
using NerdStore.Core.Messages;
using System.Threading.Tasks;

namespace NerdStore.Core.Bus
{
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator mediator;

        public MediatrHandler(IMediator _mediator)
        {
            mediator = _mediator;
        }

        public async Task PublishEvent<T>(T _event) where T : Event
        {
            await mediator.Publish(_event);
        }
    }

}
