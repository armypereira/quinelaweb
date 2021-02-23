using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuinelaWeb.Web.Models.Quinelas {
    public class QuinelaResultadoViewModel {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<QuinelaResultadoDetalle> QuinelaDetalleJugadas { get; set; }
    }


    public class QuinelaResultadoDetalle {

        public int Id { get; set; }
        public string Fecha { get; set; }
        public int Numero { get; set; }
        public string Grupo { get; set; }
        public string PaisLocal { get; set; }
        public int PaisLocalGol { get; set; }
        public int PaisLocalGolResultado { get; set; }
        public string PaisVisitante { get; set; }
        public int PaisVisitanteGol { get; set; }
        public int PaisVisitanteGolResultado { get; set; }
        public int QuinelaId { get; set; }
        public string ImagePathLocal { get; set; }
        public string ImagePathVisitante { get; set; }

    }
}