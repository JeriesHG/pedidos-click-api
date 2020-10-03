using PedidoClickAPI.Data;
using PedidoClickAPI.Domain;
using PedidoClickAPI.Domain.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PedidoClickAPI.Repository.Common
{
    public class UsuarioRepository
    {
        public dynamic Login(string username, string password, int clienteId)
        {

            var db = new pedidoclick();
            db.Database.Connection.Open();

            var usuario = db.t_Usuario.Where(x => x.Usuario == username && x.Contrasena == password && x.ClienteId == clienteId)
                .Select(x => new { id = x.Id, username = x.Usuario, role = x.RolId, name = x.Nombre + " " + x.Apellido, correo = x.Correo, cliente = x.t_Cliente })
                .FirstOrDefault();

            if (usuario != null)
            {
                // Remove Existing Sessions
                var sessions = db.t_Sesion.Where(x => x.UserId == usuario.id).ToList();
                if (sessions.Count > 0)
                {
                    db.t_Sesion.RemoveRange(sessions);
                }

                t_Sesion sesion = new t_Sesion();
                sesion.Id = Guid.NewGuid();
                sesion.FechaCreo = DateTime.Now;
                sesion.FechaExpira = DateTime.Now.AddMinutes(30);
                sesion.UserId = usuario.id;
                db.t_Sesion.Add(sesion);
                db.SaveChanges();

                db.Database.Connection.Close();
                var cliente = usuario.cliente.Nombre;
                return new { token = sesion.Id, usuario.username, usuario.role, usuario.name, usuario.correo, fechaExpira = sesion.FechaExpira, cliente };
            }

            db.Database.Connection.Close();
            return new { Mensaje = "Usuario o contraseña incorrecta" };
        }

        public IEnumerable<dynamic> Lista(int clienteId)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var response = db.t_Usuario.Where(x => x.ClienteId == clienteId)
                    .Select(x => new
                    {
                        id = x.Id,
                        itemName = x.Nombre + " " + x.Apellido,
                        correo = x.Correo,
                        usuario = x.Usuario,
                        fechaCreo = x.CreadoFecha
                    })
                    .ToList();

                db.Database.Connection.Close();

                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public dynamic GetUsuarioById(int usuarioId, int clienteId)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var response = db.t_Usuario.Where(x => x.Id == usuarioId && x.ClienteId == clienteId)
                    .Select(x => new
                    {
                        id = x.Id,
                        nombre = x.Nombre,
                        apellido = x.Apellido,
                        correo = x.Correo,
                        usuario = x.Usuario,
                        contrasena = x.Contrasena,
                        rolId = x.RolId
                    })
                    .FirstOrDefault();

                db.Database.Connection.Close();

                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public BaseResponse CrearUsuario(CrearUsuarioRequest r, int clienteId)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var usuarioExiste = db.t_Usuario.Where(x => x.Usuario == r.Usuario && x.ClienteId == clienteId).FirstOrDefault();
                if (usuarioExiste != null)
                {
                    return new BaseResponse { Mensaje = "El usuario ya existe", Resultado = false };
                }

                var u = new t_Usuario();
                u.Nombre = r.Nombre;
                u.Apellido = r.Apellido;
                u.ClienteId = clienteId;
                u.RolId = r.RolId;
                u.Usuario = r.Usuario;
                u.Contrasena = r.Contrasena;
                u.Correo = r.Correo;
                u.CreadoFecha = DateTime.Now;
                u.Borrado = false;

                db.t_Usuario.Add(u);

                db.SaveChanges();

                db.Database.Connection.Close();

                return new BaseResponse { Mensaje = "Usuario Creado", Resultado = true };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al crear el usuario", Resultado = false };
            }
        }

        public BaseResponse EliminarUsuario(int usuariId, int clienteId)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var usuario = db.t_Usuario.Where(x => x.Id == usuariId && x.ClienteId == clienteId).FirstOrDefault();
                if (usuario == null)
                {
                    return new BaseResponse { Mensaje = "El usuario no existe", Resultado = false };
                }

                db.t_Usuario.Remove(usuario);

                db.SaveChanges();

                db.Database.Connection.Close();

                return new BaseResponse { Mensaje = "Usuario Eliminado", Resultado = true };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al eliminar el usuario", Resultado = false };
            }
        }

        public BaseResponse EditarUsuario(EditarUsuarioRequest r, int clienteId)
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var u = db.t_Usuario.Where(x => x.Id == r.UsuarioId && x.ClienteId == clienteId).FirstOrDefault();
                if (u == null)
                {
                    return new BaseResponse { Mensaje = "El usuario no existe", Resultado = false };
                }

                u.RolId = r.RolId;
                u.Nombre = r.Nombre;
                u.Apellido = r.Apellido;
                u.Usuario = r.Usuario;
                u.Contrasena = r.Contrasena;
                u.Correo = r.Correo;

                db.SaveChanges();

                db.Database.Connection.Close();

                return new BaseResponse { Mensaje = "Usuario Modificado", Resultado = true };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al modificar el usuario", Resultado = false };
            }
        }

        public IEnumerable<dynamic> GetRols()
        {
            try
            {
                var db = new pedidoclick();
                db.Database.Connection.Open();

                var response = db.t_Rol.Where(x => x.Borrado == false)
                    .Select(x => new
                    {
                        id = x.Id,
                        rol = x.Rol
                    })
                    .ToList();

                db.Database.Connection.Close();

                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}