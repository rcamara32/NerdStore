using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Core.Data
{
    public interface IRepositoryBase<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }

}
