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
        private readonly DomainNotificationHandler _notification;
        private readonly IMediatorHandler _mediatorHandler;

        protected Guid ClienteId = Guid.Parse("7c0e3bc3-7937-4202-8832-b2bd59c3637d");

        protected ControllerBase(INotificationHandler<DomainNotification> notification, IMediatorHandler mediatorHandler)
        {
            _notification = (DomainNotificationHandler)notification;
            _mediatorHandler = mediatorHandler;
        }

        protected bool OperacaoValida()
            => !_notification.TemNotificacao();

        protected IEnumerable<string> ObterMensagensErro()
            => _notification.ObterNotificacoes().Select(c => c.Value).ToList();

        protected void NotificarErro(string codigo, string mensagem)
            => _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
    }
}
