using Abp.Application.Services;
using QuinelaWeb.Usuarios.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinelaWeb.Usuarios {
    public interface IUsuarioAppService:IApplicationService {
        Task<UsuarioDto> RegisterAsync(UsuarioDto valUsuarioDto);
        UsuarioDto GetByCorreo(string valCorreo);
        UsuarioDto Get(int valId);
    }
}
