using Microsoft.Extensions.DependencyInjection;
using System;

namespace Eum.Core.Module
{
    public enum ModulePriorites : uint
    {
        Low = 0,
        Medium = 1,
        High  = 2,
    }

    public interface IEumModule
    {
        ModulePriorites Priorite { get; }

        void ConfigureServices(IServiceCollection services);

        void ApplicationLoaded(IServiceProvider serviceProvider);
    }
}
