using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using QuinelaWeb.Configuration.Dto;

namespace QuinelaWeb.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : QuinelaWebAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
