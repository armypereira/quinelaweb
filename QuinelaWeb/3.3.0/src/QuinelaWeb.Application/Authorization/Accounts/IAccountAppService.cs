using System.Threading.Tasks;
using Abp.Application.Services;
using QuinelaWeb.Authorization.Accounts.Dto;

namespace QuinelaWeb.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
