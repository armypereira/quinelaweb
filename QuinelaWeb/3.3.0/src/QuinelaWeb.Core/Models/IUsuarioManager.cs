using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinelaWeb.Models {
    public interface IUsuarioManager : IDomainService {
        Task<Usuario> CreateAsync(Usuario valUsuario);
        Usuario Get(int valId);
        Usuario GetByCorreo(string valCorreo);
    }
}
