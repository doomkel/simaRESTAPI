using System;
using System.Collections.Generic;

namespace simaRESTAPI.Models
{
    public partial class Remisiones
    {
        public string Remision { get; set; }
        public decimal Tienda { get; set; }
        public string Preorden { get; set; }
        public decimal? Orden { get; set; }
        public string Status { get; set; }
        public int? Pedido { get; set; }
    }
}
