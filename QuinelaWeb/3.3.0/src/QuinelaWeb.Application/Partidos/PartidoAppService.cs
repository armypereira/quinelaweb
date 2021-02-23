using Abp.Application.Services;
using AutoMapper;
using QuinelaWeb.Administracion;
using QuinelaWeb.Models;
using QuinelaWeb.Partidos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinelaWeb.Partidos {
   public class PartidoAppService : ApplicationService, IPartidoAppService {
        private readonly IPartidoManager _PartidoManager;

        public PartidoAppService(
            IPartidoManager InitPartidoManager) {
            _PartidoManager = InitPartidoManager;

        }
        public async Task<PartidoDto> CreateAsync(PartidoDto valPartido) {
            Partido vRecord = Mapper.Map<PartidoDto, Partido>(valPartido);
            Partido vPartido = await _PartidoManager.CreateAsync(vRecord);
            PartidoDto vResult = Mapper.Map<Partido, PartidoDto>(vPartido);
            return vResult;
        }

        public void Delete(int valId) {
            _PartidoManager.Delete(valId);
        }

        public PartidoDto Get(int valId) {
            Partido vPartido =  _PartidoManager.Get(valId);
            PartidoDto vResult = Mapper.Map<Partido, PartidoDto>(vPartido);
            return vResult;
        }

        public List<PartidoDto> GetList()
        {
            List<Partido> vPartidoList = _PartidoManager.GetList();
            List<PartidoDto> vResult = Mapper.Map<List<Partido>, List<PartidoDto>>(vPartidoList);
            return vResult;
        }

        public void Update(PartidoDto valPartido) {
            Partido vRecord = Mapper.Map<PartidoDto, Partido>(valPartido);
            _PartidoManager.Update(vRecord);
        }
    }
}
