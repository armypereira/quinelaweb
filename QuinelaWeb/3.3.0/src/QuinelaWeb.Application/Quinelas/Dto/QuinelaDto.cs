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
    [AutoMap(typeof(Quinela))]
    public class QuinelaDto : EntityDto
    {
      
       
        public string Nombre { get; set; }
        public ICollection<QuinelaDetalleJugadasDto> QuinelaDetalleJugadas { get; set; }
    }
}
