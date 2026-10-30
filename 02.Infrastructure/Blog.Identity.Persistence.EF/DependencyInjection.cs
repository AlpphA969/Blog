using Autofac;
using Blog.Identity.Persistence.EF.Contracst;
using Servisec;

namespace Blog.Identity.Persistence.EF;

public class DependencyInjection : Module
{
    public static void Register(ContainerBuilder builder)
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
        
        builder.RegisterType<EFIdentityUnitOfWork>()
            .As<IIdentityUnitOfWork>()
            .InstancePerLifetimeScope();
        
        
    }
    
}