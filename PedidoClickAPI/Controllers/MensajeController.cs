using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using PedidoClickAPI.Repository;

namespace PedidoClickAPI.Controllers
{
    public class MensajeController : ApiController
    {
        string token = "1b986ba1-b2fb-4939-a57e-ea111e33823a";
        MensajesRepository mensajesRepository = new MensajesRepository();

        [HttpPost]
        [ActionName("guardar")]
        public IHttpActionResult Guardar([FromBody]dynamic request)
        {
            try
            {
                //var value = Convert.ToString(Request.Headers.Authorization.Parameter);
                var value = Convert.ToString(Request.Headers.Authorization);

                if (string.IsNullOrWhiteSpace(value))
                    return BadRequest("Token is null or empty");

                if (value != token)
                    return BadRequest("Bad token");

                var result = mensajesRepository.SaveMensaje(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [ActionName("get")]
        public IHttpActionResult Get()
        {
            try
            {
                var value = Convert.ToString(Request.Headers.Authorization.Parameter);

                if (string.IsNullOrWhiteSpace(value))
                    return BadRequest("Token is null or empty");

                if (value != token)
                    return BadRequest("Bad token");

                var result = mensajesRepository.GetMensajes();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
