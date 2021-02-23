using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinelaWeb.Models {
    [Table("Quinela")]
    public class Quinela : FullAuditedEntity, IMustHaveTenant {
        public int TenantId { get; set; }
        public string Nombre { get; set; }
        public Quinela() {
            QuinelaDetalleJugadas = new Collection<QuinelaDetalleJugadas>();
        }
      
        public virtual ICollection<QuinelaDetalleJugadas> QuinelaDetalleJugadas { get; set; }
    }
}
