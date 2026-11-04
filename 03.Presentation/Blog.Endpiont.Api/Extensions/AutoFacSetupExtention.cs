using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace Blog.Endpiont.Api.Extensions;

public static class AutoFacSetupExtention
{
    public static IHostBuilder AddAuroFac(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        hostBuilder.ConfigureContainer<ContainerBuilder>(container =>
        {
            container.RegisterModule<PersistenceEF.DependencyInjection>();
            container.RegisterModule<Infrastructure.DependencyInjection>();
            container.RegisterModule<Blog.Identity.Persistence.EF.DependencyInjection>();
            container.RegisterModule<Services.DependencyInjection>();
            
        });
        
        return hostBuilder;
    }
    
}