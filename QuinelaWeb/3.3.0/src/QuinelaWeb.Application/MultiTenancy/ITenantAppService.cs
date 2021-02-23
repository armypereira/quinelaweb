using Abp.Application.Services;
using Abp.Application.Services.Dto;
using QuinelaWeb.MultiTenancy.Dto;

namespace QuinelaWeb.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
