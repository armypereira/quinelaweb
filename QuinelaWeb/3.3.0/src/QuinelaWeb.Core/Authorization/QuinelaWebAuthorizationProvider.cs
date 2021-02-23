using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace QuinelaWeb.Authorization
{
    public class QuinelaWebAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, QuinelaWebConsts.LocalizationSourceName);
        }
    }
}
