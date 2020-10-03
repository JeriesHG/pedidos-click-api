using PedidoClickAPI.Data;
using PedidoClickAPI.Domain;
using PedidoClickAPI.Domain.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PedidoClickAPI.Repository.Common;

namespace PedidoClickAPI.Repository.Cliente
{
    public class ProductoRepository
    {
        public IEnumerable<dynamic> Lista(int clienteId)
        {
            var db = new pedidoclick();
            db.Database.Connection.Open();
            //.Where(x => x.Borrado == false)

            var lista = db.t_Categoria
                .Where(x => x.Borrado == false && x.t_Usuario.ClienteId == clienteId) // Listar productos del cliente Id
                .Select(x => new
                {
                    id = x.Id,
                    categoria = x.Categoria,
                    total = 0,
                    productos = x.t_Producto
                    .Where(p => p.IdCategoria == x.Id && p.Borrado == false && p.Activo == true)
                    .Select(p => new
                    {
                        productoId = p.Id,
                        productoNombre = p.Producto,
                        categoria = x.Categoria,
                        descripcion = p.Descripcion,
                        activo = p.Activo,
                        fechaCreo = p.FechaCreo,

                        precio = p.Precio,
                        cantidad = 0,
                        total = 0
                    }).ToList()
                })
                .OrderBy(x => x.id)
                .ToList();

            db.Database.Connection.Close();

            return lista;
        }

        public dynamic GetProductoById(int productoId, int clienteId)
        {
            var db = new pedidoclick();
            db.Database.Connection.Open();
            //.Where(x => x.Borrado == false)

            var producto = db.t_Producto
                .Where(x => x.Borrado == false && x.t_Usuario.ClienteId == clienteId && x.Id == productoId) // Listar productos del cliente Id
                .Select(x => new
                {
                    productoId = x.Id,
                    categoriaId = x.IdCategoria,
                    producto = x.Producto,
                    descripcion = x.Descripcion,
                    precio = x.Precio,
                    costo = x.Costo,
                    activo = x.Activo,
                    imagen = x.Imagen
                })
                .FirstOrDefault();

            db.Database.Connection.Close();

            return producto;
        }

        public BaseResponse GuardarProducto(int usuarioId, int clienteId, int categoriaId, Producto prod)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                if (prod.productoId == null)
                {
                    // nuevo
                    var producto = new t_Producto();
                    producto.Producto = prod.producto;
                    producto.Descripcion = prod.descripcion;
                    producto.Activo = prod.activo;
                    producto.Precio = prod.precio;
                    producto.Costo = prod.costo;
                    producto.Imagen = prod.imagen;
                    producto.FechaCreo = DateTime.Now;
                    producto.Borrado = false;
                    producto.IdCategoria = categoriaId;
                    producto.IdUsuarioCreo = usuarioId;
                    producto.IdTienda = db.t_Categoria.Where(x => x.Id == categoriaId).FirstOrDefault().IdTienda;

                    db.t_Producto.Add(producto);
                    db.SaveChanges();
                    db.Database.Connection.Close();
                    return new BaseResponse { Mensaje = "Producto Guardado", Resultado = true, Objeto = producto.Id };
                }
                else
                {
                    // existente
                    var producto = db.t_Producto.Where(x => x.Id == prod.productoId && x.t_Categoria.IdCliente == clienteId).FirstOrDefault();
                    if (producto == null)
                    {
                        return new BaseResponse { Mensaje = "El producto no se encontro en la base de datos", Resultado = false };
                    }

                    producto.Producto = prod.producto;
                    producto.Descripcion = prod.descripcion;
                    producto.Activo = prod.activo;
                    producto.Precio = prod.precio;
                    producto.Costo = prod.costo;
                    producto.Imagen = prod.imagen;

                    db.SaveChanges();
                    db.Database.Connection.Close();
                    return new BaseResponse { Mensaje = "Producto Actualizado", Resultado = true, Objeto = producto.Id };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al guardar el producto", Resultado = false };
            }
        }

        public BaseResponse ActualizarImagenUrl(int productoId, int clienteId, string url)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                // existente
                var producto = db.t_Producto.Where(x => x.Id == productoId && x.t_Categoria.IdCliente == clienteId).FirstOrDefault();
                if (producto == null)
                {
                    return new BaseResponse { Mensaje = "El producto no se encontro en la base de datos", Resultado = false };
                }
                producto.Imagen = url;

                db.SaveChanges();
                db.Database.Connection.Close();
                return new BaseResponse { Mensaje = "Imagen Actualizada", Resultado = true };

            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al actualizar la imagen", Resultado = false };
            }
        }

        public IEnumerable<dynamic> GetProductos(int clienteId)
        {
            var db = new pedidoclick();
            db.Database.Connection.Open();
            //.Where(x => x.Borrado == false)

            var lista = db.t_Producto
                .Where(x => x.Borrado == false && x.Activo == true && x.t_Categoria.IdCliente == clienteId) // Listar productos del cliente Id
                .Select(x => new
                {
                    productoId = x.Id,
                    categoriaId = x.IdCategoria,
                    categoriaTiendaId = x.t_Tienda.t_CategoriaTienda.Id,
                    //categoriaTienda = x.t_Tienda.t_CategoriaTienda.CategoriaTienda,
                    producto = x.Producto,
                    descripcion = x.Descripcion,
                    imagen = x.Imagen,

                    precio = x.Precio,
                    cantidad = 1,
                    total = 0,
                    agregado = false
                })
                .OrderBy(x => x.productoId)
                .ToList();

            db.Database.Connection.Close();

            return lista;
        }

        public IEnumerable<dynamic> BuscarProductos(int clienteId, string texto)
        {
            var db = new pedidoclick();
            db.Database.Connection.Open();
            //.Where(x => x.Borrado == false)

            var lista = db.t_Producto
                .Where(x => x.Borrado == false && x.Activo == true && x.t_Categoria.IdCliente == clienteId
                            && (x.Producto.ToLower().Contains(texto) || x.t_Categoria.Categoria.ToLower().Contains(texto))) // Listar productos del cliente Id
                .Select(x => new
                {
                    productoId = x.Id,
                    categoriaId = x.IdCategoria,
                    categoria = x.t_Categoria.Categoria,
                    producto = x.Producto,
                    descripcion = x.Descripcion,
                    imagen = x.Imagen,

                    precio = x.Precio,
                    costo = x.Costo,
                    cantidad = 0,
                    total = 0,
                    agregado = false
                })
                .OrderBy(x => x.productoId)
                .ToList();

            db.Database.Connection.Close();

            return lista;
        }

        public BaseResponse GetProductosByTienda(int clienteId, int tiendaId)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var productos = db.t_Producto
                    .Where(x =>
                    x.Borrado == false &&
                    x.Activo == true &&
                    x.t_Categoria.IdCliente == clienteId &&
                    x.IdTienda == tiendaId)
                    .Select(x => new
                    {
                        productoId = x.Id,
                        categoriaId = x.IdCategoria,
                        producto = x.Producto,
                        descripcion = x.Descripcion,
                        imagen = x.Imagen,

                        precio = x.Precio,
                        cantidad = 1,
                        total = 0,
                        agregado = false
                    })
                    .OrderBy(x => x.productoId)
                    .ToList();

                db.Database.Connection.Close();

                return new BaseResponse { Mensaje = "Productos encontrados", Resultado = true, Objeto = productos };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al recuperar los productos", Resultado = false };
            }
        }
    }
}