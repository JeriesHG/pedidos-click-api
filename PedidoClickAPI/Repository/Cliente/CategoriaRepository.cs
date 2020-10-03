using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using PedidoClickAPI.Data;
using PedidoClickAPI.Domain;
using PedidoClickAPI.Domain.Cliente;
using PedidoClickAPI.Repository.Common;

namespace PedidoClickAPI.Repository.Cliente
{
    public class CategoriaRepository
    {
        public BaseResponse Crear(dynamic request, int userId, int clienteId)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();


                var categoria = Convert.ToString(request.categoria);
                int tiendaId = request.tiendaId != null ? Convert.ToInt32(request.tiendaId) : 0;

                var c = new t_Categoria();
                c.IdCliente = clienteId;
                c.IdTienda = tiendaId;
                c.Categoria = categoria;
                c.IdUsuarioCreo = userId;
                c.FechaCreo = DateTime.Now;
                c.Borrado = false;

                db.t_Categoria.Add(c);
                db.SaveChanges();
                //db.SP_Categoria_Crear(userToken, categoria, tiendaId);

                db.Database.Connection.Close();

                return new BaseResponse { Mensaje = "Categoria Creada", Resultado = true, Objeto = c.Id };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al crear la categoria: " + ex.Message, Resultado = false };
            }
        }

        public BaseResponse Eliminar(int categoriaId, int clienteId, int usuarioId)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var categoria = db.t_Categoria.Where(x => x.Id == categoriaId && x.IdCliente == clienteId).FirstOrDefault();
                if (categoria == null)
                {
                    return new BaseResponse { Mensaje = "La categoria no se encontro en la base de datos", Resultado = false };
                }

                categoria.Borrado = true;
                categoria.FechaBorrado = DateTime.Now;
                categoria.IdUsuarioBorro = usuarioId;

                db.SaveChanges();

                db.Database.Connection.Close();
                return new BaseResponse { Mensaje = "Categoria Eliminada", Resultado = true };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al eliminar la categoria: " + ex.Message, Resultado = false };
            }
        }
        public BaseResponse ActualizarImagenUrl(int categoriaId, int clienteId, string url)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                // existente
                var categoria = db.t_Categoria.Where(x => x.Id == categoriaId && x.IdCliente == clienteId).FirstOrDefault();
                if (categoria == null)
                {
                    return new BaseResponse { Mensaje = "La categoria no se encontro en la base de datos", Resultado = false };
                }
                categoria.Imagen = url;

                db.SaveChanges();
                db.Database.Connection.Close();
                return new BaseResponse { Mensaje = "Imagen Actualizada", Resultado = true };

            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al actualizar la imagen", Resultado = false };
            }
        }

        public IEnumerable<dynamic> Categorias(int clienteId, int tiendaId)
        {
            var db = new pedidoclick();
            db.Database.Connection.Open();

            //string token = Convert.ToString(userToken);

            var response = db.SP_Categoria_List(clienteId, tiendaId).ToList();
            db.Database.Connection.Close();

            return response;
        }

        public dynamic GetCategoriaById(int categoriaId)
        {
            var db = new pedidoclick();
            db.Database.Connection.Open();

            var categoria = db.t_Categoria
                .Where(x => x.Id == categoriaId)
                .Select(x => new
                {
                    id = x.Id,
                    tiendaId = x.IdTienda,
                    categoria = x.Categoria,
                    imagen = x.Imagen,
                    productos = x.t_Producto
                    .Where(p => p.IdCategoria == categoriaId && p.Borrado == false)
                    .Select(p => new
                    {
                        productoId = p.Id,
                        productoNombre = p.Producto,
                        descripcion = p.Descripcion,
                        precio = p.Precio,
                        activo = p.Activo,
                        fechaCreo = p.FechaCreo
                    }).ToList()
                })
                .FirstOrDefault();

            db.Database.Connection.Close();

            return categoria;
        }

        public BaseResponse Actualizar(ActualizarCategoriaRequest request, string userToken)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var sesionRep = new SesionRepository();
                int? userId = sesionRep.GetUserIdFromSession(Guid.Parse(userToken));
                var idCategoria = Convert.ToInt32(request.IdCategoria);

                var categoriaDB = db.t_Categoria.FirstOrDefault(x => x.Id == idCategoria);
                if (categoriaDB == null)
                    return new BaseResponse { Mensaje = "La categoria no existe en la base de datos.", Resultado = false };

                // Actualizar nombre
                categoriaDB.Categoria = request.Categoria;

                List<t_Producto> nuevosProductosDB = new List<t_Producto>();
                var nuevosProductos = request.Productos.Where(x => x.Nuevo);
                foreach (var p in nuevosProductos)
                {
                    nuevosProductosDB.Add(new t_Producto()
                    {
                        IdCategoria = idCategoria,
                        Producto = p.ProductoNombre,
                        Descripcion = p.Descripcion,
                        Precio = p.Precio,
                        Activo = p.Activo,
                        FechaCreo = DateTime.Now,
                        IdUsuarioCreo = userId,
                        Borrado = false
                    });
                }

                db.t_Producto.AddRange(nuevosProductosDB);

                var productosActualizar = request.Productos.Where(x => x.ProductoId > 0);
                foreach (var producto in productosActualizar)
                {
                    var p = db.t_Producto.Where(x => x.Id == producto.ProductoId).FirstOrDefault();
                    p.Producto = producto.ProductoNombre;
                    p.Descripcion = producto.Descripcion;
                    p.Activo = producto.Activo;
                    p.Borrado = producto.Borrado;
                    p.Precio = producto.Precio;

                    p.FechaCreo = DateTime.Now;
                    p.IdCategoria = idCategoria;
                    p.IdUsuarioCreo = userId;
                }

                db.SaveChanges();

                db.Database.Connection.Close();

                return new BaseResponse { Mensaje = "Categoria Actualizada", Resultado = true, Objeto = categoriaDB.Id };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al actualizar la categoria: " + ex.Message, Resultado = false };
            }
        }

        public IEnumerable<dynamic> GetCategorias(int clienteId)
        {
            var db = new pedidoclick();
            db.Database.Connection.Open();

            //string token = Convert.ToString(userToken);

            var response = db.t_Categoria.Where(x => x.IdCliente == clienteId && x.Borrado == false)
                .Select(x => new
                {
                    categoriaId = x.Id,
                    categoria = x.Categoria,
                    numeroProductos = x.t_Producto.Where(c => c.Borrado == false && c.Activo == true).Count()
                }).ToList();

            db.Database.Connection.Close();

            return response;
        }

        public IEnumerable<dynamic> GetCategoriasByTienda(int clienteId, int tiendaId)
        {
            var db = new pedidoclick();
            db.Database.Connection.Open();

            //string token = Convert.ToString(userToken);

            var response = db.t_Categoria.Where(x => x.IdCliente == clienteId && x.Borrado == false && x.IdTienda == tiendaId)
                .Select(x => new
                {
                    categoriaId = x.Id,
                    categoria = x.Categoria,
                    numeroProductos = x.t_Producto.Where(c => c.Borrado == false && c.Activo == true).Count()
                }).ToList();

            db.Database.Connection.Close();

            return response;
        }
    }
}
