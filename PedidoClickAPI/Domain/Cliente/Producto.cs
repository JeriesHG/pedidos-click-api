using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PedidoClickAPI.Domain.Cliente
{
    public class Producto
    {
        public int? productoId { get; set; }
        public string producto { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public decimal costo { get; set; }
        public bool activo { get; set; }
        public string imagen { get; set; }
    }
}