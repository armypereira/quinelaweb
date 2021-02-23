using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Galac.CRM.Configuracion;
using Galac.CRM.Tools;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galac.CRM.Ventas {
    [Table("ClienteDetalleDireccion")]
    public class ClienteDetalleDireccion : FullAuditedEntity {
        public int TenantId { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public eTipoDireccion TipoDireccion { get; set; }

        [NotMapped]
        public string TipoDireccionString {
            get { return TipoDireccion.ToString(); }
            private set { TipoDireccion = value.ParseEnum<eTipoDireccion>(); }
        }

        public enum eTipoDireccion {
            [Description("Fiscal")] Fiscal,
            [Description("Instalación")] Instalacion,
            [Description("Despacho")] Despacho,
            [Description("Otro")] Otro
        }

        [ForeignKey("Parroquia")]
        public int ParroquiaId { get; set; }
        public Parroquia Parroquia { get; set; }
        
        [Display(Name = "Ubigeo")]
        [StringLength(10, ErrorMessage = "Cantidad máxima permitida 10 caracteres.")]
        public string Ubigeo { get; set; }

        [ForeignKey("Ciudad")]
        public int CiudadId { get; set; }
        public Ciudad Ciudad { get; set; }
        
        [Display(Name = "Direccion")]
        [StringLength(3000, ErrorMessage = "Cantidad máxima permitida 3000 caracteres.")]
        public string Direccion { get; set; }

        [Display(Name = "PuntoDeReferencia")]
        [StringLength(100, ErrorMessage = "Cantidad máxima permitida 100 caracteres.")]
        public string PuntoDeReferencia { get; set; }

        [Display(Name = "ZonaPostal")]
        [StringLength(7, ErrorMessage = "Cantidad máxima permitida 7 caracteres.")]
        public string ZonaPostal { get; set; }
    }
}