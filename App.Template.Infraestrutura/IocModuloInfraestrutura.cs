using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;
using System.Data.SqlClient;
using System.Data;

namespace App.Template.Infraestrutura
{
    /// <summary>
    ///     Registers types for this application and performs infrastructure configuration.
    /// </summary>
    public class IocModuloInfraestrutura : Module
    {        
        protected override void Load(ContainerBuilder builder)
        {
            ConfigureRegistrations(builder);
            base.Load(builder);
        }

        private void ConfigureRegistrations(ContainerBuilder builder)
        {
            builder.Register(ctx => ctx as IServiceProvider ?? ctx.Resolve<ILifetimeScope>() as IServiceProvider).InstancePerLifetimeScope().AsSelf();

            
        }        
    }
}