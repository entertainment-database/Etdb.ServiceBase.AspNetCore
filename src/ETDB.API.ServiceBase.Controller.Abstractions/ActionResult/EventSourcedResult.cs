﻿using System.Net;
using System.Threading.Tasks;
using ETDB.API.ServiceBase.Controller.Abstractions.Response;
using Microsoft.AspNetCore.Mvc;

namespace ETDB.API.ServiceBase.Controller.Abstractions.ActionResult
{
    public class EventSourcedResult : IActionResult
    {
        private readonly EventSourcedRepsonse eventSourcedRepsonse;

        public EventSourcedResult(EventSourcedRepsonse eventSourcedRepsonse)
        {
            this.eventSourcedRepsonse = eventSourcedRepsonse;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(this.eventSourcedRepsonse)
            {
                StatusCode = this.eventSourcedRepsonse.Success
                    ? ((EventSourcedResponseSuccess) this.eventSourcedRepsonse).Data != null
                        ? (int) HttpStatusCode.OK
                        : (int) HttpStatusCode.NoContent
                    : (int) HttpStatusCode.BadRequest
            };

            await result.ExecuteResultAsync(context);
        }
    }
}