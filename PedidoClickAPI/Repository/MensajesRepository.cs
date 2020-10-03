using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using PedidoClickAPI.Data;
using PedidoClickAPI.Domain;

namespace PedidoClickAPI.Repository
{
    public class MensajesRepository
    {
        public IEnumerable<dynamic> GetMensajes()
        {
            var db = new pedidoclick();
            db.Database.Connection.Open();

            var mensajes = db.t_Mensajes.Select(x => new
            {
                nombre = x.Nombre,
                correo = x.Correo,
                telefono = x.Telefono,
                empresa = x.Empresa,
                mensaje = x.Mensaje,
                fecha = x.FechaCreacion
            })
            .OrderBy(x => x.fecha);

            db.Database.Connection.Close();
            return mensajes;
        }

        public BaseResponse SaveMensaje(dynamic request)
        {
            try
            {
                string nombre = request.nombre;
                string correo = request.correo;
                string telefono = request.telefono;
                string empresa = request.empresa;
                string mensaje = request.mensaje;
                DateTime fecha = DateTime.Now;

                var db = new pedidoclick();
                db.Database.Connection.Open();

                var m = new t_Mensajes();
                m.Nombre = nombre;
                m.Correo = correo;
                m.Telefono = telefono;
                m.Empresa = empresa;
                m.Mensaje = mensaje;
                m.FechaCreacion = fecha;
                db.t_Mensajes.Add(m);
                db.SaveChanges();
                db.Database.Connection.Close();

                return new BaseResponse { Mensaje = "Mensaje enviado", Resultado = true };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Mensaje = "Sucedio un error al enviar el mensaje: " + ex.Message, Resultado = false };
            }

        }
    }
}