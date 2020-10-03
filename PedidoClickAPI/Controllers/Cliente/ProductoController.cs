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
    public class ProductoController : ApiController
    {
        ProductoRepository productoRepository = new ProductoRepository();

        [HttpGet]
        [ActionName("lista")]
        public IHttpActionResult Lista()
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                var result = productoRepository.Lista(clienteId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("getproductobyid")]
        public IHttpActionResult GetProductoById(dynamic d)
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                int producto = Convert.ToInt32(d.productoId);

                var result = productoRepository.GetProductoById(producto, clienteId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("guardar")]
        public IHttpActionResult Guardar([FromBody]dynamic r)
        {
            try
            {
                Guid token = Guid.Parse(Convert.ToString(Request.Headers.GetValues("Authorization").FirstOrDefault()));
                var sesionRep = new SesionRepository();
                var userId = sesionRep.GetUserIdFromSession(token);
                if (userId == null)
                    return Unauthorized();

                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                int categoriaId = Convert.ToInt32(r.categoriaId);
                Producto prod = new Producto();
                prod.activo = r.producto.activo != null ? Convert.ToBoolean(r.producto.activo) : false;
                prod.descripcion = Convert.ToString(r.producto.descripcion);
                prod.imagen = Convert.ToString(r.producto.imagen);
                prod.precio = Convert.ToDecimal(r.producto.precio);
                prod.costo = Convert.ToDecimal(r.producto.costo);
                prod.producto = Convert.ToString(r.producto.producto);
                prod.productoId = r.producto.productoId == null ? null : Convert.ToInt32(r.producto.productoId);
                //prod.productoId = Convert.ToInt32(r.producto.productoId);

                var result = productoRepository.GuardarProducto(Convert.ToInt32(userId), clienteId, categoriaId, prod);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("actualizarimagen")]
        public IHttpActionResult ActualizarImagen([FromBody]dynamic r)
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                //int productoId = Convert.ToInt32(r.productoId.Id);
                int productoId = Convert.ToInt32(r.productoId);
                string url = r.url == null ? "" : Convert.ToString(r.url);

                var result = productoRepository.ActualizarImagenUrl(productoId, clienteId, url);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [ActionName("getproductos")]
        public IHttpActionResult GetProductos()
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                var result = productoRepository.GetProductos(clienteId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [SesionController]
        [ActionName("buscar")]
        public IHttpActionResult Buscar([FromBody]dynamic r)
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                string texto = Convert.ToString(r.texto);
                var resultado = productoRepository.BuscarProductos(clienteId, texto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ActionName("getproductosbytienda")]
        public IHttpActionResult GetProductosByTienda([FromBody] int tiendaId)
        {
            try
            {
                int clienteId = Convert.ToInt32(Request.Headers.GetValues("Cliente").FirstOrDefault());
                var resultado = productoRepository.GetProductosByTienda(clienteId, tiendaId);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
