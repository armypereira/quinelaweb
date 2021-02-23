using System.Threading.Tasks;
using Abp.Application.Services;
using QuinelaWeb.Sessions.Dto;

namespace QuinelaWeb.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
