using PedidoClickAPI.Data;
using PedidoClickAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PedidoClickAPI.Repository.Common
{
    public class SesionRepository
    {
        public Sesion GetSesion(Guid sesionId)
        {
            Sesion sesion = null;

            var db = new pedidoclick();
            db.Database.Connection.Open();

            sesion = db.t_Sesion.Where(x => x.Id == sesionId).Select(x => new Sesion { Id = x.Id, FechaExpira = x.FechaExpira }).FirstOrDefault();
            db.Database.Connection.Close();

            return sesion;
        }

        public bool Update(Guid sessionId, DateTime? fechaExpira)
        {
            var db = new pedidoclick();
            db.Database.Connection.Open();

            var sesion = db.t_Sesion.FirstOrDefault(x => x.Id == sessionId);
            if (sesion != null)
            {
                sesion.FechaExpira = fechaExpira;
                db.Database.Connection.Close();
                return true;
            }

            db.Database.Connection.Close();

            return false;
        }

        public int? GetUserIdFromSession(Guid token)
        {
            var db = new pedidoclick();
            db.Database.Connection.Open();

            var sesion = db.t_Sesion.FirstOrDefault(x => x.Id == token);
            if (sesion != null)
            {
                db.Database.Connection.Close();
                return sesion.UserId;
            }

            db.Database.Connection.Close();

            return null;
        }
    }
}