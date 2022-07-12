using System;
using System.Collections.Generic;

namespace simaRESTAPI.Models
{
    public partial class Usuarios
    {
        public int CodUsuario { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string TiendaBase { get; set; }
        public int Perfil { get; set; }
    }
}
