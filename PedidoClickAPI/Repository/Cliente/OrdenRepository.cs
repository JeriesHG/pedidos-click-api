using PedidoClickAPI.Data;
using PedidoClickAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PedidoClickAPI.Domain.Cliente;

namespace PedidoClickAPI.Repository.Cliente
{
    public class OrdenRepository
    {
        public BaseResponse Crear(CrearOrdenRequest request, int clienteId)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var orden = new t_Orden();
                orden.IdOrdenEstado = 1; // Pendiente
                orden.Nombre = request.nombre;
                orden.Telefono = request.telefono;
                orden.CorreoElectronico = request.correo;
                orden.Identidad = request.identidad;
                orden.Calle = request.calle;
                orden.Avenida = request.avenida;
                orden.Colonia = request.colonia;
                orden.Ciudad = request.ciudad;
                orden.Comentario = request.comentario;
                orden.FechaCreo = DateTime.Now;
                orden.IdCliente = clienteId;

                db.t_Orden.Add(orden);
                db.SaveChanges();

                if (orden.Id <= 0)
                {
                    db.Database.Connection.Close();
                    return new BaseResponse { Mensaje = "Sucedio un error al generar la orden", Resultado = false };
                }

                var ordenDetalle = new List<t_OrdenDetalle>();
                foreach (var d in request.productos)
                {
                    var detalle = new t_OrdenDetalle();
                    detalle.IdProducto = d.productoId;
                    detalle.Precio = d.precio;
                    detalle.Cantidad = d.cantidad;
                    detalle.IdOrden = orden.Id;
                    detalle.FechaCreo = DateTime.Now;

                    ordenDetalle.Add(detalle);
                }

                db.t_OrdenDetalle.AddRange(ordenDetalle);
                db.SaveChanges();

                db.Database.Connection.Close();

                return new BaseResponse { Mensaje = "Orden Generada", Resultado = true };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al generar la orden", Resultado = false };
            }
        }

        public IEnumerable<dynamic> Lista(int clienteId, List<int> estados)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var response = db.t_Orden
                    .Where(x => estados.Contains(x.IdOrdenEstado.Value) && x.IdCliente == clienteId)
                    .Select(x => new
                    {
                        id = x.Id,
                        cliente = x.Nombre,
                        telefono = x.Telefono,
                        idEstado = x.t_OrdenEstado.Id,
                        estado = x.t_OrdenEstado.Estado,
                        fechaCreo = x.FechaCreo,
                        asignadoA = x.t_Usuario.Nombre + " " + x.t_Usuario.Apellido
                    })
                    .OrderBy(x => x.fechaCreo)
                    .ToList();

                db.Database.Connection.Close();

                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public dynamic GetOrdenById(int ordenId)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var response = db.t_Orden
                    .Where(x => x.Id == ordenId)
                    .Select(x => new
                    {
                        id = x.Id,
                        cliente = x.Nombre,
                        telefono = x.Telefono,
                        correo = x.CorreoElectronico,
                        identidad = x.Identidad,
                        calle = x.Calle,
                        avenida = x.Avenida,
                        colonia = x.Colonia,
                        ciudad = x.Ciudad,
                        comentario = x.Comentario,
                        idEstado = x.t_OrdenEstado.Id,
                        estado = x.t_OrdenEstado.Estado,
                        fechaCreo = x.FechaCreo,
                        detalle = x.t_OrdenDetalle.Where(d => d.IdOrden == x.Id && (d.Borrado == false || d.Borrado == null))
                        .Select(d => new
                        {
                            categoria = d.t_Producto.t_Categoria.Categoria,
                            idProducto = d.IdProducto,
                            producto = d.t_Producto.Producto,
                            precio = d.Precio,
                            cantidad = d.Cantidad,
                            total = d.Precio * d.Cantidad
                        }),
                        total = x.t_OrdenDetalle.Where(d => d.IdOrden == x.Id).Select(d => new { total = d.Precio * d.Cantidad }).Sum(d => d.total)
                    })
                    .OrderBy(x => x.fechaCreo)
                    .FirstOrDefault();

                db.Database.Connection.Close();

                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<dynamic> GetEstados()
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var response = db.t_OrdenEstado
                    .Select(x => new
                    {
                        id = x.Id,
                        itemName = x.Estado,
                        estado = "Estados"
                    })
                    .OrderBy(x => x.id)
                    .ToList();

                db.Database.Connection.Close();

                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public BaseResponse AsignarOrden(int ordenId, int usuarioId, int estadoId)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var orden = db.t_Orden.Where(x => x.Id == ordenId).FirstOrDefault();
                if (orden == null)
                {
                    return new BaseResponse { Mensaje = "No se pudo encontrar la orden", Resultado = false };
                }

                if (usuarioId > 0)
                    orden.IdAsignado = usuarioId;
                else
                    orden.IdAsignado = null;

                orden.IdOrdenEstado = estadoId;

                db.SaveChanges();

                db.Database.Connection.Close();

                return new BaseResponse { Mensaje = "Orden Asignada", Resultado = true };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al asignar la orden", Resultado = false };
            }
        }

        public BaseResponse EditarOrden(int ordenId, int usuarioId, ActualizarOrdenRequest request)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var orden = db.t_Orden.Where(x => x.Id == ordenId).FirstOrDefault();
                if (orden == null)
                {
                    return new BaseResponse { Mensaje = "No se pudo encontrar la orden", Resultado = false };
                }

                foreach (var p in request.productos.Where(x => x.agregar == false).ToList())
                {
                    var producto = db.t_OrdenDetalle.Where(x => x.IdOrden == ordenId && x.IdProducto == p.productoId).FirstOrDefault();
                    if (producto != null)
                    {
                        producto.Cantidad = p.cantidad;

                        if (p.borrado)
                        {
                            producto.Borrado = p.borrado;
                            producto.IdUsuarioBorro = usuarioId;
                            producto.FechaBorro = DateTime.Now;
                        }
                    }
                }

                var productosAgregar = new List<t_OrdenDetalle>();
                foreach (var p in request.productos.Where(x => x.agregar == true).ToList())
                {
                    var prod = new t_OrdenDetalle();
                    prod.IdProducto = p.productoId;
                    prod.Cantidad = p.cantidad;
                    prod.FechaCreo = DateTime.Now;
                    prod.IdOrden = ordenId;
                    prod.Borrado = false;

                    var productoDB = db.t_Producto.FirstOrDefault(x => x.Id == p.productoId);
                    if (productoDB != null)
                        prod.Precio = productoDB.Precio;

                    productosAgregar.Add(prod);
                }

                if (productosAgregar.Count > 0)
                    db.t_OrdenDetalle.AddRange(productosAgregar);

                db.SaveChanges();

                db.Database.Connection.Close();

                return new BaseResponse { Mensaje = "Orden Actualizada", Resultado = true };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al actualizar la orden", Resultado = false };
            }
        }
    }
}