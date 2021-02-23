using System.Collections.Generic;
using QuinelaWeb.Roles.Dto;
using QuinelaWeb.Users.Dto;

namespace QuinelaWeb.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}