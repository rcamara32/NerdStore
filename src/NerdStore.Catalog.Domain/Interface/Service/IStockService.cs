using System;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain.Interface.Service
{
    public interface IStockService : IDisposable
    {
        Task<bool> Debit(Guid productId, int quantity);
        Task<bool> Replenishment(Guid productId, int quantity);

    }
}
