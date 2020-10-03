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
    public class CategoriaTiendaRepository
    {
        public BaseResponse Crear(dynamic request, int userId, int clienteId)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();


                var categoria = Convert.ToString(request.categoria);
                bool activo = Convert.ToBoolean(request.activo);

                var c = new t_CategoriaTienda();
                c.IdCliente = clienteId;
                c.CategoriaTienda = categoria;
                c.Activo = activo;
                c.IdUsuarioCreo = userId;
                c.FechaCreo = DateTime.Now;
                c.Borrado = false;

                db.t_CategoriaTienda.Add(c);
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

                var categoria = db.t_CategoriaTienda.Where(x => x.Id == categoriaId && x.IdCliente == clienteId).FirstOrDefault();
                if (categoria == null)
                {
                    return new BaseResponse { Mensaje = "La categoria no se encontro en la base de datos", Resultado = false };
                }

                categoria.Borrado = true;
                categoria.FechaBorro = DateTime.Now;
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
                var categoria = db.t_CategoriaTienda.Where(x => x.Id == categoriaId && x.IdCliente == clienteId).FirstOrDefault();
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
        public BaseResponse Actualizar(int categoriaId, int clienteId, dynamic categoriaData)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var categoria = db.t_CategoriaTienda
                    .Where(x => x.Id == categoriaId && x.IdCliente == clienteId)
                    .FirstOrDefault();

                if (categoria == null)
                {
                    return new BaseResponse { Mensaje = "La categoria no se encontro en la base de datos", Resultado = false };
                }

                string categoriaTienda = Convert.ToString(categoriaData.categoria);
                bool activo = Convert.ToBoolean(categoriaData.activo);

                categoria.CategoriaTienda = categoriaTienda;
                categoria.Activo = activo;

                db.SaveChanges();
                db.Database.Connection.Close();

                return new BaseResponse { Mensaje = "Categoria Actualizada", Resultado = true, Objeto = categoria.Id };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al actualizar la categoria: " + ex.Message, Resultado = false };
            }
        }
        public dynamic GetCategoriaById(int categoriaId, int clienteId)
        {
            var db = new pedidoclick();
            db.Database.Connection.Open();

            var response = db.t_CategoriaTienda.Where(x => x.IdCliente == clienteId && x.Borrado == false)
                .Select(x => new
                {
                    categoriaId = x.Id,
                    categoria = x.CategoriaTienda,
                    fechaCreo = x.FechaCreo,
                    activo = x.Activo,
                    imagen = x.Imagen
                }).FirstOrDefault();

            db.Database.Connection.Close();

            //return categoria;
            return response;
        }
        public IEnumerable<dynamic> GetCategorias(int clienteId)
        {
            var db = new pedidoclick();
            db.Database.Connection.Open();

            //string token = Convert.ToString(userToken);

            var response = db.t_CategoriaTienda.Where(x => x.IdCliente == clienteId && x.Borrado == false)
                .Select(x => new CategoriaTienda
                {
                    categoriaId = x.Id,
                    categoria = x.CategoriaTienda,
                    activo = x.Activo,
                    imagen = x.Imagen,
                    fechaCreo = x.FechaCreo,
                    numeroProductos = 0                    
                }).ToList();

            foreach (var c in response)
            {
                int categoriaId = c.categoriaId;
                int _numeroProductos = 0;

                var tiendas = db.t_Tienda.Where(x => x.IdCategoriaTienda == c.categoriaId && x.Borrado == false).ToList();

                foreach (var t in tiendas)
                {
                    var productos = db.t_Producto.Where(x => x.IdTienda == t.Id).ToList();
                    _numeroProductos += productos.Count;
                }

                var _categoriaActualizar = response.FirstOrDefault(x => x.categoriaId == categoriaId);
                _categoriaActualizar.numeroProductos = _numeroProductos;
            }

            db.Database.Connection.Close();

            return response;
        }
        public IEnumerable<dynamic> GetTiendas(int categoriaId, int clienteId)
        {
            var db = new pedidoclick();
            db.Database.Connection.Open();

            //string token = Convert.ToString(userToken);

            var response = db.t_Tienda
                .Where(x =>
                x.IdCategoriaTienda == categoriaId &&
                x.IdCliente == clienteId &&
                x.Borrado == false
                )
                .Select(x => new
                {
                    tiendaId = x.Id,
                    tienda = x.Nombre,
                    logo = x.Logo,
                    banner = x.Banner,
                    observaciones = x.Observaciones,
                    paginaWeb = x.Paginaweb
                }).ToList();

            db.Database.Connection.Close();

            return response;
        }
    }
}
