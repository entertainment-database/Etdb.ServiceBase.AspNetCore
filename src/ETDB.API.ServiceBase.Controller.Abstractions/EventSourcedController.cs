﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Handler;
using Microsoft.AspNetCore.Mvc;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Notifications;

namespace ETDB.API.ServiceBase.Controller.Abstractions
{
    public abstract class EventSourcedController : ControllerBase
    {
        private readonly IDomainNotificationHandler<DomainNotification> notificationHandler;

        protected EventSourcedController(IDomainNotificationHandler<DomainNotification> notificationHandler)
        {
            this.notificationHandler = notificationHandler;
        }

        protected IEnumerable<DomainNotification> Notifications
            => notificationHandler.GetNotifications();

        protected bool IsValidOperation()
        {
            return !notificationHandler.HasNotifications();
        }

        protected EventSourcedResult ExecuteResult(EventSourcedDTO result = null)
        {
            if (this.IsValidOperation())
            {
                return new EventSourcedResult(new EventSourcedResponseSuccess
                {
                    Data = result
                });
            }

            return new EventSourcedResult(new EventSourcedResponseFail
            {
                Errors = this.notificationHandler
                    .GetNotifications()
                    .Select(error => error.Value)
                    .ToArray()
            });
        }

        //protected new EventSourcedResult Response(EventSourcedDTO result = null)
        //{
        //    if (this.IsValidOperation())
        //    {
        //        return new EventSourcedResult(new EventSourcedResponseSuccess
        //        {
        //            Data = result
        //        });
        //    }

        //    return new EventSourcedResult(new EventSourcedResponseFail
        //    {
        //        Errors = this.notificationHandler
        //            .GetNotifications()
        //            .Select(error => error.Value)
        //            .ToArray()
        //    });
        //}

        protected void NotifyModelStateErrors()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                var erroMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected void NotifyError(string code, string message)
        {
            notificationHandler.Handle(new DomainNotification(code, message));
        }
    }
}
