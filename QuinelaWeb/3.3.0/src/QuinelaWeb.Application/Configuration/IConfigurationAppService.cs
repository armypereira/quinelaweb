using System.Threading.Tasks;
using Abp.Application.Services;
using QuinelaWeb.Configuration.Dto;

namespace QuinelaWeb.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}