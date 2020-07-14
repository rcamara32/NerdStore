using NerdStore.Core.Messages;
using System;
using System.Collections.Generic;

namespace NerdStore.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        //private List<Event> _notifications;
        //public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        //public void AddEvents(Event evennt) 
        //{
        //    _notifications = _notifications ?? new List<Event>();
        //    _notifications.Add(evennt);
        //}

        //public void RemoveEvent(Event evennt)
        //{
        //    _notifications?.Remove(evennt);
        //}

        //public void ClearEvents()
        //{
        //    _notifications?.Clear();
        //}

        public override bool Equals(object obj)
        {
            var comparTo = obj as Entity;

            if (ReferenceEquals(this, comparTo)) return true;
            if (ReferenceEquals(null, comparTo)) return false;

            return Id.Equals(comparTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)

        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 311) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id= {Id}]";
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }

    }
}
