using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace NerdStore.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evt) where T : Event;
        Task<bool> SendCommand<T>(T cmd) where T : Command;
        Task PublishNotification<T>(T notification) where T : DomainNotification;

    }

}
