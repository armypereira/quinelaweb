using Abp.Domain.Services;
using QuinelaWeb.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinelaWeb.Models {
    public interface IPartidoManager : IDomainService {
        Partido  Get(int valId);
        void Update(Partido valPartido);
        void Delete(int valId);
        Task<Partido> CreateAsync(Partido valPartido);
        List<Partido> GetList();

    }
}
