using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QuinelaWeb.Models {
    public class UsuarioManager : IUsuarioManager {
        private readonly IRepository<Usuario> _UsuarioRepository;
        public UsuarioManager(IRepository<Usuario> InitUsuarioRepository) {
            _UsuarioRepository = InitUsuarioRepository;
        }

        public async Task<Usuario> CreateAsync(Usuario valUsuario) {
            var vResult = await _UsuarioRepository.InsertAsync(valUsuario);

            return vResult;


        }

        public Usuario Get(int valId) {
            return _UsuarioRepository.Get(valId);
        }

        public Usuario GetByCorreo(string valCorreo) {
            return _UsuarioRepository.Query(p => p.Where(x => x.Correo == valCorreo)).FirstOrDefault();
        }

        string GetBody(string valUsuari, string valClave) {
            string vResult = string.Empty;
             

            return vResult;
        }
    }

}
