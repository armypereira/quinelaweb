using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using QuinelaWeb.EntityFramework;

namespace QuinelaWeb
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(QuinelaWebCoreModule))]
    public class QuinelaWebDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<QuinelaWebDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
