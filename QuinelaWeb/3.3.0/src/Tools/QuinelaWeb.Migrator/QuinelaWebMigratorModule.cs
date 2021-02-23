using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using QuinelaWeb.EntityFramework;

namespace QuinelaWeb.Migrator
{
    [DependsOn(typeof(QuinelaWebDataModule))]
    public class QuinelaWebMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<QuinelaWebDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}