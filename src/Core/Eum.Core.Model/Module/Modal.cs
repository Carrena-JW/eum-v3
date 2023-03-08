using Eum.Core.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Module
{
    public static class ModuleExtensions
    {
        public static void ConfigureModuleServices<T>(this IServiceCollection service, Assembly assembly)
        {
            var serviceTypes = ModuleHelper.GetImplementationTypes<T>(assembly);
            foreach (var type in serviceTypes)
            {
                var allInterfaces = type.GetTypeInfo().ImplementedInterfaces;
                var interfaces = allInterfaces.Where(@i => @i != typeof(T));
                RegisterServiceAttribute attribute = null;
                Type @interface = interfaces.FirstOrDefault(t => t.GetCustomAttribute<RegisterServiceAttribute>() != null);
                if (@interface is null)
                {
                    @interface = interfaces
                        .Where(t => !new string[] { "IDisposable", "IDataService", "IDataRepository" }.Contains(t.Name))
                        .LastOrDefault();
                }
                else attribute = @interface.GetCustomAttribute<RegisterServiceAttribute>();

                if (@interface.GetCustomAttribute<IgnoreRegisterAttribute>() != null) continue;
                if (@interface != null)
                {
                    if (@interface.GenericTypeArguments.Length > 0)
                        @interface = @interface.GetGenericTypeDefinition();

                    switch (attribute?.Option ?? RegisterServiceOptions.Singleton)
                    {
                        case RegisterServiceOptions.Scoped:
                            service.AddScoped(@interface, type);
                            break;

                        case RegisterServiceOptions.Transient:
                            service.AddTransient(@interface, type);
                            break;

                        default:
                            service.AddSingleton(@interface, type);
                            break;
                    }
                }
                else
                {
                    switch (attribute?.Option ?? RegisterServiceOptions.Singleton)
                    {
                        case RegisterServiceOptions.Scoped:
                            service.AddScoped(type);
                            break;

                        case RegisterServiceOptions.Transient:
                            service.AddTransient(type);
                            break;

                        default:
                            service.AddSingleton(type);
                            break;
                    }
                }
            }
        }

        public static void ConfigureServices(this IServiceCollection service, Assembly assembly = null)
        {
            if (assembly == null) assembly = Assembly.GetCallingAssembly();
            service.ConfigureModuleServices<IService>(assembly);
        }

        public static void ConfigureRepositories(this IServiceCollection service, Assembly assembly = null)
        {
            if (assembly == null) assembly = Assembly.GetCallingAssembly();
            service.ConfigureModuleServices<IRepository>(assembly);
        }
    }
}
