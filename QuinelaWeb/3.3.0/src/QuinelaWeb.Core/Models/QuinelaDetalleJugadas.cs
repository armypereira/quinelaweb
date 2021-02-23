using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinelaWeb.Models {
    [Table("QuinelaDetalleJugadas")]
    public class QuinelaDetalleJugadas : FullAuditedEntity, IMustHaveTenant {
        public int TenantId { get; set; }
        public int Numero { get; set; }
        public string Grupo { get; set; }
        public string PaisLocal { get; set; }
        public int PaisLocalGol { get; set; }
        public string PaisVisitante { get; set; }
        public int PaisVisitanteGol { get; set; }
        public int QuinelaId { get; set; }
        public Quinela Quinela { get; set; }

    }
}
