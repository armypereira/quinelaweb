using Abp.Application.Services;
using Abp.Domain.Repositories;
using AutoMapper;
using QuinelaWeb.Models;
using QuinelaWeb.Usuarios.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinelaWeb.Usuarios {
    public class UsuarioAppService : ApplicationService, IUsuarioAppService {
        private readonly IUsuarioManager _UsuarioManager;
        public UsuarioAppService(
            IUsuarioManager InitUsuarioManager) {
            _UsuarioManager = InitUsuarioManager;
       
        }

        public UsuarioDto Get(int valId) {
            Usuario vUsuario = _UsuarioManager.Get(valId);
            UsuarioDto vResult = Mapper.Map<Usuario, UsuarioDto>(vUsuario);
            return vResult;
        }

        public UsuarioDto GetByCorreo(string valCorreo) {
            Usuario vUsuario = _UsuarioManager.GetByCorreo(valCorreo);
            UsuarioDto vResult = Mapper.Map<Usuario, UsuarioDto>(vUsuario);
            return vResult;
        }

        public async  Task<UsuarioDto> RegisterAsync(UsuarioDto valUsuarioDto) {
            Usuario vRecord = Mapper.Map<UsuarioDto, Usuario>(valUsuarioDto);
            Usuario vUsuario = await _UsuarioManager.CreateAsync(vRecord);
            UsuarioDto vResult = Mapper.Map<Usuario, UsuarioDto>(vUsuario);
            return vResult;
           
        }
    }
}
