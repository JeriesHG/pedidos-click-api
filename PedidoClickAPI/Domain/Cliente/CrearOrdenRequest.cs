using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PedidoClickAPI.Domain.Cliente
{
    public class CrearOrdenRequest
    {
        public string avenida { get; set; }
        public string calle { get; set; }
        public string ciudad { get; set; }
        public string colonia { get; set; }
        public string comentario { get; set; }
        public string correo { get; set; }
        public string identidad { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public List<Producto> productos { get; set; }

        public class Producto
        {
            public int productoId { get; set; }
            public decimal precio { get; set; }
            public int cantidad { get; set; }
        }
    }
}