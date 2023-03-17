using AutoMapper;
using Eum.Core.Service.Contracts.ServiceDesk.Product;
using Eum.gRPC.Server.ServiceDesk.Modules.ProductModule.Domains;

namespace Eum.gRPC.Server.ServiceDesk
{
    public static class MapperConfig
    {
        public static void AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                //Configuring Employee and EmployeeDTO
                cfg.CreateMap<Product, ProductDTO>().ReverseMap();
                //Any Other Mapping Configuration ....
            });
        }
    }
}
