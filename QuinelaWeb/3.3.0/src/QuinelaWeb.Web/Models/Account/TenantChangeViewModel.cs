using Abp.AutoMapper;
using QuinelaWeb.Sessions.Dto;

namespace QuinelaWeb.Web.Models.Account
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}