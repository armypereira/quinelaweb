using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Galac.CRM.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galac.CRM.Ventas {
    [Table("Cliente")]
    public class Cliente : FullAuditedEntity, IMustHaveTenant {
        public Cliente() {
            ClienteDetalleContactos = new Collection<ClienteDetalleContacto>();
            ClienteDetalleDirecciones = new Collection<ClienteDetalleDireccion>();
            ClienteDetalleDocumentos = new Collection<ClienteDetalleDocumento>();
        }

        public int TenantId { get; set; }

        [Required]
        [Display(Name = "N° Identificación")]
        [StringLength(20, ErrorMessage = "Cantidad máxima permitida 20 caracteres.")]
        public string NroIdentificacion { get; set; }

        [Required]
        [StringLength(170, ErrorMessage = "Cantidad máxima permitida 170 caracteres.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        public bool EsExtranjero { get; set; }

        public eEsContribuyente EsContribuyente { get; set; }

        [NotMapped]
        public string EsContribuyenteString {
            get { return EsContribuyente.ToString(); }
            private set { EsContribuyente = value.ParseEnum<eEsContribuyente>(); }
        }

        public enum eEsContribuyente {
            [Description("Contribuyente")] Contribuyente,
            [Description("No Contribuyente")] [Display(Name = "No Contribuyente")] NoContribuyente
        }

        public eTipoDeSujeto TipoDeSujeto { get; set; }

        [NotMapped]
        public string TipoDeSujetoString {
            get { return TipoDeSujeto.ToString(); }
            private set { TipoDeSujeto = value.ParseEnum<eTipoDeSujeto>(); }
        }

        public enum eTipoDeSujeto {
            [Description("Persona Natural")] [Display(Name = "Persona Natural")] PersonaNatural,
            [Description("Persona Jurídica")] [Display(Name = "Persona Jurídica")] PersonaJuridica

        }

        [Display(Name = "Observaciones")]
        [StringLength(1000, ErrorMessage = "Cantidad máxima permitida 1000 caracteres.")]
        public string Observacion { get; set; }

        [StringLength(40, ErrorMessage = "Cantidad máxima permitida 40 caracteres.")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [StringLength(40, ErrorMessage = "Cantidad máxima permitida 40 caracteres.")]
        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [StringLength(200, ErrorMessage = "Cantidad máxima permitida 200 caracteres.")]
        [Display(Name = "Email")]
        public string CorreoElectronico { get; set; }

        public eTipoDocumento TipoDocumento { get; set; }

        [NotMapped]
        public string TipoDocumentoString {
            get { return TipoDocumento.ToString(); }
            private set { TipoDocumento = value.ParseEnum<eTipoDocumento>(); }
        }

        public enum eTipoDocumento {
            [Description("Id Fiscal")] [Display(Name = "Id Fiscal")] IdFiscal,
            [Description("N° Documento de Identificación")] [Display(Name = "N° Documento de Identificación")] NroDocumentoIdentificacion,
            [Description("Carnet de Extranjería")] [Display(Name = "Carnet de Extranjería")] CarnetExtranjeria,
            [Description("Pasaporte")] Pasaporte,
            [Description("Otros Documentos")] [Display(Name = "Otros Documentos")] OtrosDocumetnos
        }

        [ForeignKey("Vendedor")]
        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }

        [ForeignKey("SectorNegocio")]
        public int? SectorNegocioId { get; set; }
        public SectorDeNegocio SectorNegocio { get; set; }
        public eNivelDePrecio NivelDePrecio { get; set; }

        [NotMapped]
        public string NivelDePrecioString {
            get { return NivelDePrecio.ToString(); }
            private set { NivelDePrecio = value.ParseEnum<eNivelDePrecio>(); }
        }

        public enum eNivelDePrecio {
            [Description("Precio 1")] [Display(Name = "Precio 1")] Precio1,
            [Description("Precio 2")] [Display(Name = "Precio 2")] Precio2,
            [Description("Precio 3")] [Display(Name = "Precio 3")] Precio3,
            [Description("Precio 4")] [Display(Name = "Precio 4")] Precio4,
            [Description("No Asignado")] [Display(Name = "No Asignado")] NoAsignado

        }
        public eStatusCliente Status { get; set; }
        public eStatusClienteCRM StatusCRM { get; set; }
        public bool EstaSincronizado { get; set; }
        public DateTime FechaSincronizacion { get; set; }

        [NotMapped]
        public string StatusClienteString {
            get { return Status.ToString(); }
            private set { Status = value.ParseEnum<eStatusCliente>(); }
        }

        public enum eStatusCliente {
            [Description("Activo")] Activo,
            [Description("Inactivo")] Inactivo,
            [Description("Restringido")] Restringido,
            [Description("Suspendido")] Suspendido,
            [Description("Tiempo Sin Contacto")] [Display(Name = "Tiempo Sin Contacto")] TiempoSinContacto,
            [Description("Datos Desactualizados")] [Display(Name = "Datos Desactualizados")] DatosDesactualizados
        }

        public enum eStatusClienteCRM {
            [Description("Prospecto")] Prospecto,
            [Description("Definitivo")] Definitivo,
            [Description("Interesado")] Interesado
        }

        public virtual ICollection<ClienteDetalleContacto> ClienteDetalleContactos { get; set; }
        public virtual ICollection<ClienteDetalleDireccion> ClienteDetalleDirecciones { get; set; }
        public virtual ICollection<ClienteDetalleDocumento> ClienteDetalleDocumentos { get; set; }
    }
}
