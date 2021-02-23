using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinelaWeb.Models
{
    public interface IQuinelaManager : IDomainService
    {
        Quinela Get(int valId);
        void Update(Quinela valQuinela);
        void Delete(int valId);
        Task<Quinela> CreateAsync(Quinela valQuinela);
        List<Quinela> GetList();
        List<Quinela> GetList(long valIdUsuario);
    }
}
