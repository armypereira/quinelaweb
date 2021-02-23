using Abp.Application.Services;
using AutoMapper;
using Microsoft.AspNet.Identity;
using QuinelaWeb.Authorization.Users;
using QuinelaWeb.Models;
using QuinelaWeb.Quinelas.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinelaWeb.Quinelas {
    public class QuinelaAppService : ApplicationService, IQuinelaAppService {

        private readonly IQuinelaManager _QuinelaManager;
        private readonly UserManager _userManager;

        public QuinelaAppService(
            IQuinelaManager InitQuinelaManager, UserManager userManager) {
            _QuinelaManager = InitQuinelaManager;
            _userManager = userManager;

        }

        public async Task<QuinelaDto> CreateAsync(QuinelaDto valQuinelaDto) {
            Quinela vRecord = new Quinela();
            QuinelaDto vResult = new QuinelaDto();
            try {
                vRecord = Mapper.Map<QuinelaDto, Quinela>(valQuinelaDto);
                var user = _userManager.Users.Where(u => u.Id == AbpSession.UserId).ToList().FirstOrDefault();
                vRecord.Nombre = user.Name + "_" + DateTime.Now.ToString();
                Quinela vQuinela = await _QuinelaManager.CreateAsync(vRecord);
                vResult = Mapper.Map<Quinela, QuinelaDto>(vQuinela);
            } catch (Exception xe) {
                string a = xe.Message;
            }
            return vResult;
        }

        public void Delete(int valId) {
            try {

                List<Quinela> vList = _QuinelaManager.GetList(Convert.ToInt64(AbpSession.UserId));
               if( vList.Where(p=>p.Id== valId).Count() > 0) {
                    _QuinelaManager.Delete(valId);
                }
               
            } catch (Exception xe) {
                string a = xe.Message;
            }
        }

        public QuinelaDto Get(int valId) {
            QuinelaDto vResult = new QuinelaDto();
            try {
                List<Quinela> vList = _QuinelaManager.GetList(Convert.ToInt64(AbpSession.UserId));
                Quinela vQuinela = vList.Where(p => p.Id == valId).FirstOrDefault();
                vResult = Mapper.Map<Quinela, QuinelaDto>(vQuinela);
            } catch (Exception xe) {
                string a = xe.Message;
            }
            return vResult;
        }

        public List<QuinelaDto> GetList() {
            List<QuinelaDto> vResult = new List<QuinelaDto>();
            try {
             
                List<Quinela> vList = _QuinelaManager.GetList();
                vResult = Mapper.Map<List<Quinela>, List<QuinelaDto>>(vList);
            } catch (Exception xe) {
                string a = xe.Message;
            }
            return vResult;
        }

        public List<QuinelaDto> GetListByUser() {
            List<QuinelaDto> vResult = new List<QuinelaDto>();
            try {
                var user = _userManager.Users.Where(u => u.Id == AbpSession.UserId).ToList().FirstOrDefault();
                List<Quinela> vList = _QuinelaManager.GetList(user.Id);
                vResult = Mapper.Map<List<Quinela>, List<QuinelaDto>>(vList);
            } catch (Exception xe) {
                string a = xe.Message;
            }
            return vResult;
        }

        public void Update(QuinelaDto valQuinelaDto) {
            QuinelaDto vResult = new QuinelaDto();
            try {
                Quinela vRecord = Mapper.Map<QuinelaDto, Quinela>(valQuinelaDto);
                vRecord.TenantId = Convert.ToInt32(AbpSession.TenantId);
                _QuinelaManager.Update(vRecord);
            } catch (Exception xe) {
                string a = xe.Message;
            }
        }
    }
}
