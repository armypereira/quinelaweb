using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinelaWeb.Models {
    public class Usuario : FullAuditedEntity, IMustHaveTenant {
        public int TenantId { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public bool Invitado { get; set; }
        public string CorreoAnfitrion { get; set; }
    }
}
