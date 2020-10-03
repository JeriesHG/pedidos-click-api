using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PedidoClickAPI.Domain.Cliente
{
    public class ActualizarCategoriaRequest
    {
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }
        public List<Producto> Productos { get; set; }

        public class Producto
        {
            public int ProductoId { get; set; }
            //public double Id { get; set; }
            public string ProductoNombre { get; set; }
            public string Descripcion { get; set; }
            public decimal Precio { get; set; }
            public bool Activo { get; set; }
            public bool Borrado { get; set; }
            public bool Nuevo { get; set; }
        }
    }
}