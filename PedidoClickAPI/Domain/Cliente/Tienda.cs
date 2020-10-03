using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PedidoClickAPI.Domain.Cliente
{
    public class Tienda
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCliente { get; set; }
        public int IdCategoriaTienda { get; set; }
        public string CategoriaTienda { get; set; }
        public string Logo { get; set; }
        public string Banner { get; set; }       
    }
}