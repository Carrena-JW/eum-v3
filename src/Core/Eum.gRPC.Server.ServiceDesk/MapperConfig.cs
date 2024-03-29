﻿using AutoMapper;
using Eum.Core.Service.Contracts.ServiceDesk.ClientModule.Data;
using Eum.Core.Service.Contracts.ServiceDesk.CompanyModule.Data;
using Eum.Core.Service.Contracts.ServiceDesk.ProductModule.Data;
using Eum.gRPC.Server.ServiceDesk.Modules.ClientModule;
using Eum.gRPC.Server.ServiceDesk.Modules.ClientModule.Domains;
using Eum.gRPC.Server.ServiceDesk.Modules.CompanyModule;
using Eum.gRPC.Server.ServiceDesk.Modules.CompanyModule.Domains;
using Eum.gRPC.Server.ServiceDesk.Modules.ContractModule;
using Eum.gRPC.Server.ServiceDesk.Modules.ProductModule;
using Eum.gRPC.Server.ServiceDesk.Modules.ProductModule.Domains;

namespace Eum.gRPC.Server.ServiceDesk
{
    public static class MapperConfig
    {
        public static void AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                //CaseModule.ConfigureMapper(cfg);
                ClientEndpoint.ConfigureMapper(cfg);
                CompanyEndpoint.ConfigureMapper(cfg);
                ContractEndpoint.ConfigureMapper(cfg);
                ProductEndpoint.ConfigureMapper(cfg);
            });
        }
    }
}
