using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PedidoClickAPI.Domain.Cliente
{
    public class ActualizarOrdenRequest
    {
        public int ordenId { get; set; }
        public List<Producto> productos { get; set; }

        public class Producto
        {
            public int productoId { get; set; }
            public int cantidad { get; set; }
            public bool borrado { get; set; }
            public bool agregar { get; set; }
        }
    }
}