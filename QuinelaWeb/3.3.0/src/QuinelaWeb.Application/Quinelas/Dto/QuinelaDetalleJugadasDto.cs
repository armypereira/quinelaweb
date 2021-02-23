using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using QuinelaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinelaWeb.Quinelas.Dto
{
    [AutoMap(typeof(QuinelaDetalleJugadas))]
    public class QuinelaDetalleJugadasDto : EntityDto {
      
        public int Numero { get; set; }
        public string Grupo { get; set; }
        public string PaisLocal { get; set; }
        public int PaisLocalGol { get; set; }
        public string PaisVisitante { get; set; }
        public int PaisVisitanteGol { get; set; }
        public int QuinelaId { get; set; }
        
    }
}
