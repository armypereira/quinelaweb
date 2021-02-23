using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using QuinelaWeb.Administracion;
using QuinelaWeb.Authorization.Roles;
using QuinelaWeb.Authorization.Users;
using QuinelaWeb.Models;
using QuinelaWeb.MultiTenancy;

namespace QuinelaWeb.EntityFramework
{
    public class QuinelaWebDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Quinela> Quinelas { get; set; }
        public DbSet<QuinelaDetalleJugadas> QuinelasDetalleJugadas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public QuinelaWebDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in QuinelaWebDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of QuinelaWebDbContext since ABP automatically handles it.
         */
        public QuinelaWebDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public QuinelaWebDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public QuinelaWebDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
