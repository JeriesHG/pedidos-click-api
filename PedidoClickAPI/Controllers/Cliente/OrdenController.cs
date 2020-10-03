using PedidoClickAPI.Controllers.Common;
using PedidoClickAPI.Domain.Cliente;
using PedidoClickAPI.Repository.Cliente;
using PedidoClickAPI.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PedidoClickAPI.Controllers.Cliente
{
    public class OrdenController : ApiController
    {
        OrdenRepository ordenRepository = new OrdenRepository();
        UsuarioRepository usuarioRepository = new UsuarioRepository();

        [HttpPost]
        [ActionName("crear")]
        public IHttpActionResult Crear(CrearOrdenRequest request)
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                var result = ordenRepository.Crear(request, clienteId);

                if (result.Resultado)
                {
                    Util.Util.SendEmail("Pedido Creado", request.correo, "", "El pedido ha sido ingresado al sistema.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("lista")]
        public IHttpActionResult Lista(List<int> estados)
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                var result = ordenRepository.Lista(clienteId, estados);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [SesionController]
        [ActionName("getestados")]
        public IHttpActionResult GetEstados()
        {
            try
            {
                var result = ordenRepository.GetEstados();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("getordenbyid")]
        public IHttpActionResult GetOrdenById([FromBody]int ordenId)
        {
            try
            {
                var result = ordenRepository.GetOrdenById(ordenId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [SesionController]
        [ActionName("usuarios")]
        public IHttpActionResult Usuarios()
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                var result = usuarioRepository.Lista(clienteId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("asignarorden")]
        public IHttpActionResult GetOrdenById(dynamic request)
        {
            try
            {
                var ordenId = Convert.ToInt32(request.ordenId);
                var estadoId = Convert.ToInt32(request.estadoId);
                var usuarioId = Convert.ToInt32(request.userId);

                var result = ordenRepository.AsignarOrden(ordenId, usuarioId, estadoId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("editarorden")]
        //public IHttpActionResult EditarOrden(dynamic request)
        public IHttpActionResult EditarOrden(ActualizarOrdenRequest editrequest)
        {
            try
            {
                //var ordenId = Convert.ToInt32(request.ordenId);

                Guid token = Guid.Parse(Convert.ToString(Request.Headers.GetValues("Authorization").FirstOrDefault()));
                var sesionRep = new SesionRepository();
                var userId = sesionRep.GetUserIdFromSession(token);
                if (userId == null)
                    return Unauthorized();

                //var productos = request.productos;
                //var productos = (ActualizarOrdenRequest)request.productos;
                var productos = editrequest;

                var result = ordenRepository.EditarOrden(editrequest.ordenId, Convert.ToInt32(userId), productos);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
