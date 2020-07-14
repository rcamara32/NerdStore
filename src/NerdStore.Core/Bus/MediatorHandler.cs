using MediatR;
using NerdStore.Core.Messages;
using System.Threading.Tasks;

namespace NerdStore.Core.Bus
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T evt) where T : Event
        {
            await _mediator.Publish(evt);
        }

        public async Task<bool> SendCommand<T>(T cmd) where T : Command
        {
            return await _mediator.Send(cmd);
        }
    }

}
