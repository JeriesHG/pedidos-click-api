using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PedidoClickAPI.Domain.Cliente;
using PedidoClickAPI.Repository.Common;

namespace PedidoClickAPI.Controllers.Common
{
    public class UsuarioController : ApiController
    {
        UsuarioRepository usuarioRepository = new UsuarioRepository();

        [HttpPost]
        [ActionName("login")]
        public IHttpActionResult Login([FromBody]dynamic request)
        {
            try
            {
                string usuario = request.username;
                string password = request.password;
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                var response = usuarioRepository.Login(usuario, password, clienteId);
                if (response != null)
                {
                    return Ok(response);
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("eliminar")]
        public IHttpActionResult Eliminar(dynamic r)
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                int usuarioId = Convert.ToInt32(r.usuarioId);
                var response = usuarioRepository.EliminarUsuario(usuarioId, clienteId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("getusuariobyid")]
        public IHttpActionResult GetUsuarioById(dynamic r)
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                int usuarioId = Convert.ToInt32(r.usuarioId);
                var response = usuarioRepository.GetUsuarioById(usuarioId, clienteId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("agregar")]
        public IHttpActionResult Agregar([FromBody]CrearUsuarioRequest request)
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                var response = usuarioRepository.CrearUsuario(request, clienteId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("editar")]
        public IHttpActionResult Editar([FromBody]EditarUsuarioRequest request)
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                var response = usuarioRepository.EditarUsuario(request, clienteId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [SesionController]
        [ActionName("rols")]
        public IHttpActionResult Rols()
        {
            try
            {
                var response = usuarioRepository.GetRols();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
