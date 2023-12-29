using System.Reflection;
using Autofac;
using Helper.CustomLogger;
using Logics.Base;
using Repositories.Base;
using Module = Autofac.Module;

namespace HouseManagement;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var logicAssemblies = Assembly.GetAssembly(typeof(BaseLogic));
        if (logicAssemblies != null)
        {
            builder.RegisterAssemblyTypes(logicAssemblies).AsImplementedInterfaces().SingleInstance();
        }

        var repoAssemblies = Assembly.GetAssembly(typeof(BaseRepository));
        if (repoAssemblies != null)
        {
            builder.RegisterAssemblyTypes(repoAssemblies).AsImplementedInterfaces().SingleInstance();
        }
            
        builder.RegisterType<CustomLogger>().As<ICustomLogger>().SingleInstance();
    }
}