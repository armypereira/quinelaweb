using Abp.Domain.Entities.Auditing;
using Galac.CRM.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galac.CRM.Ventas {
    [Table("ClienteDetalleDocumento")]
    public class ClienteDetalleDocumento : FullAuditedEntity {

        public int TenantId { get; set; }
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [StringLength(250, ErrorMessage = "Cantidad máxima permitida 250 caracteres.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        public eArchivoAdjuntoDataType DataType { get; set; }
        public byte[] Data { get; set; }

        [NotMapped]
        public string DataTypeString {
            get { return DataType.ToString(); }
            private set { DataType = value.ParseEnum<eArchivoAdjuntoDataType>(); }
        }

        public enum eArchivoAdjuntoDataType {
            [Description("Otros")] Otros = 0,
            [Description("Word")] Word,
            [Description("Excel")] Excel,
            [Description("PowerPoint")] PowerPoint,
            [Description("Pdf")] Pdf,
            [Description("Txt")] Txt,
            [Description("Image")] Image
        }
    }
}
