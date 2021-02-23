using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuinelaWeb.Web.Models.Partidos {
    public class PartidoViewModel {
        public int Id { get; set; }
        [Required]
        public int Numero { get; set; }
        [Required]
        public string Grupo { get; set; }
        [Required]
        public string PaisLocal { get; set; }
        [Required]
        public int PaisLocalGol { get; set; }
        [Required]
        public string PaisVisitante { get; set; }
        [Required]
        public int PaisVisitanteGol { get; set; }
    }
}