using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using PedidoClickAPI.Domain.Common;
using PedidoClickAPI.Repository.Common;

namespace PedidoClickAPI.Controllers.Common
{
    public class SesionController : ActionFilterAttribute
    {
        SesionRepository sesionRepository = new SesionRepository();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //base.OnActionExecuting(actionContext);

            //int sessionTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["SessionTimeOut"] ?? "30");
            int sessionTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["SessionTimeOut"] ?? "240");
            Sesion sesion = null;

            var h = actionContext.Request.Headers;
            if (h.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Session Doesn't Exist");
            }
            else
            {
                if (h.Authorization.Parameter == "")
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Session TimedOut");
                }
                else
                {
                    //var sessionId = Guid.Parse(h.Authorization.Parameter.Replace("Bearer ", ""));
                    var sessionId = Guid.Parse(Convert.ToString(h.Authorization));

                    sesion = sesionRepository.GetSesion(sessionId);
                    //var now = DateTime.UtcNow;
                    var now = DateTime.Now;

                    if (sesion == null)
                    {
                        actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Session Doesn't Exist");
                    }
                    else
                    {
                        if ((sesion.FechaExpira.Value - now).TotalMinutes <= 0)
                        {
                            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Session TimedOut");
                        }
                        else
                        {
                            sesion.FechaExpira = now.AddMinutes(sessionTimeOut);
                            sesionRepository.Update(sesion.Id, sesion.FechaExpira);
                        }
                    }
                }
            }

        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
