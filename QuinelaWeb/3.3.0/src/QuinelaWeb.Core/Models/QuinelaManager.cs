using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinelaWeb.Models {
    public class QuinelaManager : IQuinelaManager {
        private readonly IRepository<Quinela> _QuinelaRepository;


        public QuinelaManager(IRepository<Quinela> InitQuinelaRepository) {
            _QuinelaRepository = InitQuinelaRepository;
        }
        public async Task<Quinela> CreateAsync(Quinela valQuinela) {

            return await _QuinelaRepository.InsertAsync(valQuinela);
        }

        public void Delete(int valId) {
            _QuinelaRepository.Delete(valId);
        }

        public Quinela Get(int valId) {
            throw new NotImplementedException();
        }

        public List<Quinela> GetList() {
            return _QuinelaRepository.GetAllList().ToList();
        }

        public List<Quinela> GetList(long valIdUsuario) {
            return _QuinelaRepository.Query(p => p.Where(x => x.CreatorUserId == valIdUsuario)).ToList();
        }

        public void Update(Quinela valQuinela) {
          var vRecord=  _QuinelaRepository.Get(valQuinela.Id);
           foreach(var vDetalle in vRecord.QuinelaDetalleJugadas) {
                vDetalle.PaisLocalGol = valQuinela.QuinelaDetalleJugadas.Where(p => p.Numero == vDetalle.Numero).FirstOrDefault().PaisLocalGol;
                vDetalle.PaisVisitanteGol = valQuinela.QuinelaDetalleJugadas.Where(p => p.Numero == vDetalle.Numero).FirstOrDefault().PaisVisitanteGol;
            }
            
        }
    }
}
