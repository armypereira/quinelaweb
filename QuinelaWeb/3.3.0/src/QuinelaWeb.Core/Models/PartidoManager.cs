using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using QuinelaWeb.Administracion;

namespace QuinelaWeb.Models
{
    public class PartidoManager : IPartidoManager
    {
        private readonly IRepository<Partido> _PartidoRepository;

        public PartidoManager(IRepository<Partido> InitPartidoRepository)
        {
            _PartidoRepository = InitPartidoRepository;
        }

        public async Task<Partido> CreateAsync(Partido valPartido)
        {
            return await _PartidoRepository.InsertAsync(valPartido);
        }

        public void Delete(int valId)
        {
            var vRecord = _PartidoRepository.Get(valId);

            _PartidoRepository.Delete(vRecord);
        }

        public Partido Get(int valId)
        {
            return _PartidoRepository.Get(valId);
        }

        public List<Partido> GetList()
        {
            return _PartidoRepository.GetAllList();
        }

        public void Update(Partido valPartido)
        {
            _PartidoRepository.Update(valPartido);
        }
    }
}
