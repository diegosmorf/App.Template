using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace App.Template.Aplicacao
{
    public class IocModuloAplicacao : Module
    {
        private static readonly string BaseApplicationNamespace = string.Join(".", Assembly.GetExecutingAssembly().FullName.Split('.').Take(2));

        protected override void Load(ContainerBuilder builder)
        {
            ConfigureRegistrations(builder);

            base.Load(builder);
        }

        private void ConfigureRegistrations(ContainerBuilder builder)
        {
            builder.Register(ctx => ctx as IServiceProvider ?? ctx.Resolve<ILifetimeScope>() as IServiceProvider).InstancePerLifetimeScope().AsSelf();
            
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).Where(t => t.Name.StartsWith("App.Template.Aplicacao")).AsImplementedInterfaces();
        }
    }
}