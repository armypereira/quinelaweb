using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using QuinelaWeb.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinelaWeb.Partidos.Dto {
    [AutoMap(typeof(Partido))]
    public class PartidoDto : EntityDto {
        public int Numero { get; set; }
        public string Grupo { get; set; }
        public string PaisLocal { get; set; }
        public int PaisLocalGol { get; set; }
        public string PaisVisitante { get; set; }
        public int PaisVisitanteGol { get; set; }
    }
}
