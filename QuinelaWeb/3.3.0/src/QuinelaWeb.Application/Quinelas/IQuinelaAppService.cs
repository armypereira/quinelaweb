using Abp.Application.Services;
using QuinelaWeb.Quinelas.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinelaWeb.Quinelas
{
    public interface IQuinelaAppService : IApplicationService    {
        QuinelaDto Get(int valId);
        void Update(QuinelaDto valQuinelaDto);
        void Delete(int valId);
        Task<QuinelaDto> CreateAsync(QuinelaDto valQuinelaDto);
        List<QuinelaDto> GetList();
        List<QuinelaDto> GetListByUser();
    }
}
