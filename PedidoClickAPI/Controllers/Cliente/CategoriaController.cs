using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PedidoClickAPI.Controllers.Common;
using PedidoClickAPI.Domain.Cliente;
using PedidoClickAPI.Repository.Cliente;
using PedidoClickAPI.Repository.Common;

namespace PedidoClickAPI.Controllers.Cliente
{
    public class CategoriaController : ApiController
    {
        CategoriaRepository categoriaRepository = new CategoriaRepository();
        SesionRepository sesionRepository = new SesionRepository();

        [HttpPost]
        [SesionController]
        [ActionName("crear")]
        public IHttpActionResult Crear([FromBody]dynamic request)
        {
            try
            {
                string userToken = Convert.ToString(Request.Headers.Authorization);
                Guid token = Guid.Parse(Convert.ToString(Request.Headers.GetValues("Authorization").FirstOrDefault()));
                var userId = sesionRepository.GetUserIdFromSession(token);
                if (userId == null)
                    return Unauthorized();

                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());

                var response = categoriaRepository.Crear(request, Convert.ToInt32(userId), clienteId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("eliminar")]
        public IHttpActionResult Eliminar([FromBody] dynamic request)
        {
            try
            {
                string userToken = Convert.ToString(Request.Headers.Authorization);
                Guid token = Guid.Parse(Convert.ToString(Request.Headers.GetValues("Authorization").FirstOrDefault()));
                var userId = sesionRepository.GetUserIdFromSession(token);
                if (userId == null)
                    return Unauthorized();

                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                int categoriaId = Convert.ToInt32(request.categoriaId);

                var response = categoriaRepository.Eliminar(categoriaId, clienteId, Convert.ToInt32(userId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("listar")]
        public IHttpActionResult Listar([FromBody] dynamic request)
        {
            try
            {
                //string userToken = Convert.ToString(Request.Headers.Authorization);
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                int tiendaId = request.tiendaId != null ? Convert.ToInt32(request.tiendaId) : 0;
                var response = categoriaRepository.Categorias(clienteId, tiendaId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("getcategoriabyid")]
        public IHttpActionResult GetCategoriaById([FromBody]dynamic request)
        {
            try
            {
                var categoriaId = Convert.ToInt32(request.categoriaId);
                var response = categoriaRepository.GetCategoriaById(categoriaId);
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
        public IHttpActionResult Actualizar([FromBody]ActualizarCategoriaRequest request)
        {
            try
            {
                string userToken = Convert.ToString(Request.Headers.Authorization);
                var response = categoriaRepository.Actualizar(request, userToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Para el cliente final
        [HttpGet]
        [ActionName("getcategorias")]
        public IHttpActionResult GetCategorias()
        {
            try
            {
                //string userToken = Convert.ToString(Request.Headers.Authorization);
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                var response = categoriaRepository.GetCategorias(clienteId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ActionName("getcategoriasbytienda")]
        public IHttpActionResult GetCategoriasByTienda([FromBody] int tiendaId)
        {
            try
            {
                //string userToken = Convert.ToString(Request.Headers.Authorization);
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                var response = categoriaRepository.GetCategoriasByTienda(clienteId, tiendaId);
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
                //int productoId = Convert.ToInt32(r.productoId.Id);
                int categoriaId = Convert.ToInt32(r.categoriaId);
                string url = r.url == null ? "" : Convert.ToString(r.url);

                var result = categoriaRepository.ActualizarImagenUrl(categoriaId, clienteId, url);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
