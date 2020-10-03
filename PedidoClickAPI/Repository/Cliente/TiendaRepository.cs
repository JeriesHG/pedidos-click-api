using PedidoClickAPI.Data;
using PedidoClickAPI.Domain;
using PedidoClickAPI.Domain.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace PedidoClickAPI.Repository.Cliente
{
    public class TiendaRepository
    {

        public BaseResponse Crear(Tienda tienda)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var _tienda = new t_Tienda();
                _tienda.Nombre = tienda.Nombre;
                _tienda.IdCliente = tienda.IdCliente;
                _tienda.IdCategoriaTienda = tienda.IdCategoriaTienda;
                _tienda.Banner = tienda.Banner;
                _tienda.Logo = tienda.Logo;
                _tienda.Borrado = false;
                db.t_Tienda.Add(_tienda);

                db.SaveChanges();

                db.Database.Connection.Close();

                return new BaseResponse { Mensaje = "Tienda Creada", Resultado = true, Objeto = _tienda.Id};
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al crear la tienda: " + ex.Message, Resultado = false };
            }
        }

        public BaseResponse Actualizar(Tienda tienda)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var _tienda = db.t_Tienda.Where(x => x.Id == tienda.Id).FirstOrDefault();
                if (_tienda == null)
                {
                    return new BaseResponse { Mensaje = "La tienda no existe", Resultado = false };
                }

                _tienda.Nombre = tienda.Nombre;
                _tienda.Logo = tienda.Logo;
                _tienda.Banner = tienda.Banner;
                _tienda.IdCategoriaTienda = tienda.IdCategoriaTienda;

                db.SaveChanges();

                db.Database.Connection.Close();
                return new BaseResponse { Mensaje = "Tienda Actualizada", Resultado = true };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al actualizar la tienda: " + ex.Message, Resultado = false };
            }
        }

        public BaseResponse ActualizarImagenUrl(int tiendaId, int clienteId, string urlLogo, string urlBanner, bool actualizarLogo = false, bool actualizarBanner = false)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                // existente
                var tienda = db.t_Tienda.Where(x => x.Id == tiendaId && x.IdCliente == clienteId).FirstOrDefault();
                if (tienda == null)
                {
                    return new BaseResponse { Mensaje = "La tienda no se encontro en la base de datos", Resultado = false };
                }

                if (actualizarLogo)
                {
                    tienda.Logo = urlLogo;
                }
                if (actualizarBanner)
                {
                    tienda.Banner = urlBanner;
                }

                db.SaveChanges();
                db.Database.Connection.Close();
                return new BaseResponse { Mensaje = "Imagen Actualizada", Resultado = true };

            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al actualizar la imagen", Resultado = false };
            }
        }

        public BaseResponse Borrar(int tiendaId)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var _tienda = db.t_Tienda.Where(x => x.Id == tiendaId).FirstOrDefault();
                if (_tienda == null)
                {
                    return new BaseResponse { Mensaje = "La tienda no existe", Resultado = false };
                }

                _tienda.Borrado = true;

                db.SaveChanges();

                db.Database.Connection.Close();
                return new BaseResponse { Mensaje = "Tienda Borrada", Resultado = true };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al borrar la tienda: " + ex.Message, Resultado = false };
            }
        }

        public BaseResponse GetTienda(int tiendaId)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var _tienda = db.t_Tienda.Where(x => x.Id == tiendaId).FirstOrDefault();
                if (_tienda == null)
                {
                    return new BaseResponse { Mensaje = "La tienda no existe", Resultado = false };
                }

                var tienda = new Tienda();
                tienda.Nombre = _tienda.Nombre;
                tienda.Logo = _tienda.Logo;
                tienda.Banner = _tienda.Banner;
                tienda.IdCliente = Convert.ToInt32(_tienda.IdCliente);
                tienda.IdCategoriaTienda = Convert.ToInt32(_tienda.IdCategoriaTienda);

                db.Database.Connection.Close();
                return new BaseResponse { Mensaje = "Tienda Encontrada", Resultado = true, Objeto = tienda };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al buscar la tienda: " + ex.Message, Resultado = false };
            }
        }

        public BaseResponse GetTiendas(int clienteId)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var _tiendas = db.t_Tienda.Where(x => x.IdCliente == clienteId).ToList();

                var tiendas = new List<Tienda>();

                foreach (var t in _tiendas)
                {
                    var tienda = new Tienda();
                    tienda.Id = t.Id;
                    tienda.Nombre = t.Nombre;
                    tienda.Logo = t.Logo;
                    tienda.Banner = t.Banner;
                    tienda.IdCliente = Convert.ToInt32(t.IdCliente);
                    tienda.IdCategoriaTienda = Convert.ToInt32(t.IdCategoriaTienda);
                    if (tienda.IdCategoriaTienda > 0)
                        tienda.CategoriaTienda = db.t_CategoriaTienda.Where(x => x.Id == tienda.IdCategoriaTienda && x.IdCliente == clienteId).FirstOrDefault().CategoriaTienda;
                    tiendas.Add(tienda);
                }

                db.Database.Connection.Close();
                return new BaseResponse { Mensaje = $"Tienda Encontradas {tiendas.Count}", Resultado = true, Objeto = tiendas };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al buscar las tiendas: " + ex.Message, Resultado = false };
            }
        }
    }
}