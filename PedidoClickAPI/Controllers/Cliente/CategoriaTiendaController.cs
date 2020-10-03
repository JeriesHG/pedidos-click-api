using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PedidoClickAPI.Controllers.Common;
using PedidoClickAPI.Repository.Cliente;
using PedidoClickAPI.Repository.Common;

namespace PedidoClickAPI.Controllers.Cliente
{
    public class CategoriaTiendaController : ApiController
    {
        CategoriaTiendaRepository CategoriaTiendaRepository = new CategoriaTiendaRepository();
        SesionRepository sesionRepository = new SesionRepository();

        [HttpGet]
        [ActionName("listar")]
        public IHttpActionResult Listar()
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                var response = CategoriaTiendaRepository.GetCategorias(clienteId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("crear")]
        public IHttpActionResult Crear([FromBody] dynamic request)
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());

                string userToken = Convert.ToString(Request.Headers.Authorization);
                Guid token = Guid.Parse(Convert.ToString(Request.Headers.GetValues("Authorization").FirstOrDefault()));
                var userId = sesionRepository.GetUserIdFromSession(token);
                if (userId == null)
                    return Unauthorized();

                var response = CategoriaTiendaRepository.Crear(request, Convert.ToInt32(userId), clienteId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("actualizar")]
        public IHttpActionResult Actualizar([FromBody] dynamic request)
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());

                string userToken = Convert.ToString(Request.Headers.Authorization);
                Guid token = Guid.Parse(Convert.ToString(Request.Headers.GetValues("Authorization").FirstOrDefault()));
                var userId = sesionRepository.GetUserIdFromSession(token);
                if (userId == null)
                    return Unauthorized();

                int categoriaId = Convert.ToInt32(request.categoriaTiendaId);
                var response = CategoriaTiendaRepository.Actualizar(categoriaId, clienteId, request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("actualizarimagen")]
        public IHttpActionResult ActualizarImagen([FromBody] dynamic r)
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                int categoriaTiendaId = Convert.ToInt32(r.categoriaTiendaId);
                string url = r.url == null ? "" : Convert.ToString(r.url);

                var result = CategoriaTiendaRepository.ActualizarImagenUrl(categoriaTiendaId, clienteId, url);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ActionName("getcategoriatienda")]
        public IHttpActionResult GetCategoriaTienda([FromBody] int categoriaTiendaId)
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                var response = CategoriaTiendaRepository.GetCategoriaById(categoriaTiendaId, clienteId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
