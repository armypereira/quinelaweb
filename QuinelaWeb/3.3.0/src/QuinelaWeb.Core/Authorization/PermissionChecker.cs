using Abp.Authorization;
using QuinelaWeb.Authorization.Roles;
using QuinelaWeb.Authorization.Users;

namespace QuinelaWeb.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
