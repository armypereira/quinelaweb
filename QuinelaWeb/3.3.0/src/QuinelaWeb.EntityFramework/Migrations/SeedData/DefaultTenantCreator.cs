using System.Linq;
using QuinelaWeb.EntityFramework;
using QuinelaWeb.MultiTenancy;

namespace QuinelaWeb.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly QuinelaWebDbContext _context;

        public DefaultTenantCreator(QuinelaWebDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
