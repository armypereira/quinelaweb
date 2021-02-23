using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuinelaWeb.Web.Models.Quinelas
{
    public class QuinelaViewModel
    {
        
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<QuinelaDetalle> QuinelaDetalleJugadas { get; set; }
    }

    public class QuinelaDetalle {
    
        public int Id { get; set; }
        public string Fecha { get; set; }
        public int Numero { get; set; }
        public string Grupo { get; set; }
        public string PaisLocal { get; set; }
        public int PaisLocalGol { get; set; }
        public string PaisVisitante { get; set; }
        public int PaisVisitanteGol { get; set; }
        public int QuinelaId { get; set; }
        public string ImagePathLocal { get; set; }
        public string ImagePathVisitante { get; set; }

    }
}