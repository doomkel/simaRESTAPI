using System;
using System.Collections.Generic;

namespace simaRESTAPI.Models
{
    public partial class Imagenes
    {
        public int Id { get; set; }
        public string Estilo { get; set; }
        public byte[] Imagen { get; set; }
    }
}
