using System;

namespace NerdStore.Core.Messages.CommonMessages.Domainevents
{
    public class DomainEvent : Event
    {
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }

    }
}
