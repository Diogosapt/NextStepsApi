using MediatR;
using System;
using System.Collections.Generic;

namespace NextSteps.Business.Core.Common
{
    public record Entity
    {
        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public string CreatedBy { get; init; }

        public DateTime CreateDate { get; init; }

        public string UpdatedBy { get; init; }

        public DateTime UpdateDate { get; init; }
    }
}