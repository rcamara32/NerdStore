using System;

namespace NerdStore.Core.Messages
{
    public abstract class Message
    {
        //public MessageType MessageType { get; protected set; }
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;           
        }

    }

}
