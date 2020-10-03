using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PedidoClickAPI.Domain.Common
{
    public class Sesion
    {
        public Guid Id { get; set; }
        public DateTime? FechaExpira { get; set; }
    }
}