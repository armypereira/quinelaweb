using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using QuinelaWeb.Roles.Dto;
using QuinelaWeb.Users.Dto;

namespace QuinelaWeb.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}