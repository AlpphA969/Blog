using Autofac;
using Servisec;

namespace Blog.PersistenceEF;

public class DependencyInjection : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        builder.RegisterAssemblyTypes(assembly)
            .Where(type => type.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        builder.RegisterAssemblyTypes(assembly)
            .Where(type => type.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<EFUnitOfWork>()
            .As<IUnitOfWork>()
            .InstancePerLifetimeScope();
    }
    
}