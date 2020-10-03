using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PedidoClickAPI.Domain.Cliente
{
    public class CategoriaTienda
    {
        public int categoriaId { get; set; }
        public string categoria { get; set; }
        public bool? activo { get; set; }
        public string imagen { get; set; }
        public DateTime? fechaCreo { get; set; }
        public int numeroProductos { get; set; }

    }
}