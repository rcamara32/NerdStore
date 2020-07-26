using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.WebApp.MVC.Controllers
{
    public abstract class ControllerBase : Controller
    {

        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;

        public ControllerBase(INotificationHandler<DomainNotification> notifications,
                              IMediatorHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
        }

        protected Guid ClientId = Guid.Parse("1bf7dc4d-c7fc-4d8b-b724-b7302d3bb63c");

        protected bool IsOperationValid()
        {
            return (!_notifications.HasNotification());
        }

        protected IEnumerable<string> GetErrorMessages()
        {
            return _notifications.GetNotifications()
                .Select(n => n.Value).ToList();
        }

        protected void NotifyError(string code, string message)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(code, message));
        }




    }
}
