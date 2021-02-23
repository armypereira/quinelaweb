using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using QuinelaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinelaWeb.Usuarios.Dto {

    [AutoMap(typeof(Usuario))]
    public  class UsuarioDto : EntityDto  {
        public int TenantId { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public bool Invitado { get; set; }
        public string CorreoAnfitrion { get; set; }
    }
}
