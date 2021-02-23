using QuinelaWeb.EntityFramework;
using EntityFramework.DynamicFilters;

namespace QuinelaWeb.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly QuinelaWebDbContext _context;

        public InitialHostDbBuilder(QuinelaWebDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
