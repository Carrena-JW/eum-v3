using Eum.Core.Shared.Infra.Mvc.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Shared.Infra.Mvc
{
    public static class MvcExtensions
    {
        //public static IMvcBuilder ConfigureFxPlugins(this IMvcBuilder builder)
        //{
        //    return builder.ConfigureApplicationPartManager(new ApplicationPartConfiguration(builder.Services).Configure);
        //}

        //public static IMvcBuilder ConfigureFxGlobalFilters(this WebApplicationBuilder builder)
        //{

        //    return builder.AddMvcOptions(option =>
        //    {
        //        option.Filters.Add<GlobalApiResponseWrappingFilter>();
        //    });
        //}

        //public static void ConfigureFxFilters(this IServiceCollection services)
        //{
        //    services.AddScoped<FxTransactionAttribute>();
        //}
    }
}
