using NerdStore.Core.Messages;
using System.Threading.Tasks;

namespace NerdStore.Core.Bus
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evt) where T : Event;
        Task<bool> SendCommand<T>(T cmd) where T : Command;
    }

}
