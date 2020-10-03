using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PedidoClickAPI.Controllers.Common;
using PedidoClickAPI.Domain.Cliente;
using PedidoClickAPI.Repository.Cliente;

namespace PedidoClickAPI.Controllers.Cliente
{
    public class TiendaController : ApiController
    {
        TiendaRepository TiendaRepository = new TiendaRepository();

        [HttpPost]
        [SesionController]
        [ActionName("crear")]
        public IHttpActionResult Crear([FromBody] Tienda tienda)
        {
            try
            {
                string userToken = Convert.ToString(Request.Headers.Authorization);
                var response = TiendaRepository.Crear(tienda);
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
        public IHttpActionResult Actualizar([FromBody] Tienda tienda)
        {
            try
            {
                var response = TiendaRepository.Actualizar(tienda);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("borrar")]
        public IHttpActionResult Borrar([FromBody] int tiendaId)
        {
            try
            {
                var response = TiendaRepository.Borrar(tiendaId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ActionName("gettienda")]
        public IHttpActionResult GetTienda([FromBody] int tiendaId)
        {
            try
            {
                var response = TiendaRepository.GetTienda(tiendaId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [ActionName("gettiendas")]
        public IHttpActionResult GetTiendas()
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                var response = TiendaRepository.GetTiendas(clienteId);
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
                int tiendaId = Convert.ToInt32(r.tiendaId);
                string urlLogo = r.urlLogo == null ? "" : Convert.ToString(r.urlLogo);
                string urlBanner = r.urlBanner == null ? "" : Convert.ToString(r.urlBanner);
                bool actualizarLogo = r.actualizarLogo == null ? false : Convert.ToBoolean(r.actualizarLogo);
                bool actualizarBanner = r.actualizarBanner == null ? false : Convert.ToBoolean(r.actualizarBanner);

                var result = TiendaRepository.ActualizarImagenUrl(
                    tiendaId,
                    clienteId,
                    urlLogo,
                    urlBanner,
                    actualizarLogo,
                    actualizarBanner);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
