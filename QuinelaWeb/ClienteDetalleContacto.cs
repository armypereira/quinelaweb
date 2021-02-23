using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galac.CRM.Ventas {
    [Table("ClienteDetalleContacto")]
    public class ClienteDetalleContacto : FullAuditedEntity {
        public int TenantId { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int ContactoId { get; set; }
        public Contacto Contacto { get; set; }
    }
}