using Abp.Application.Services;
using QuinelaWeb.Partidos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinelaWeb.Partidos {
     public interface IPartidoAppService : IApplicationService {
        PartidoDto Get(int valId);
        void Update(PartidoDto valPartido);
        void Delete(int valId);
        Task<PartidoDto> CreateAsync(PartidoDto valPartido);
        List<PartidoDto> GetList();
    }
}
