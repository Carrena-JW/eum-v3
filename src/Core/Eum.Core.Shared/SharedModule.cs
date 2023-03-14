using Eum.Core.Module;
using Eum.Core.Shared.Infra.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Shared
{
    public class SharedModule : IEumModule
    {
        public ModulePriorites Priorite => throw new NotImplementedException();

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureRepositories();
            services.ConfigureServices();
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<GlobalApiResponseWrappingFilter>();
            });
        }

        public void ApplicationLoaded(IServiceProvider serviceProvider)
        {
        }
    }
}
