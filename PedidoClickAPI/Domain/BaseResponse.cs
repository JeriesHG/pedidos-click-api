using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PedidoClickAPI.Domain
{
    public class BaseResponse
    {
        public string Mensaje { get; set; }
        public bool Resultado { get; set; }
        public dynamic Objeto { get; set; }
    }
}