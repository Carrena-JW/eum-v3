using Eum.Core;
using Eum.Core.Module;
using Eum.Core.Service.Utilities;
using Eum.Core.Shared;
using Eum.Core.Shared.Infra.Identity.JwtAuth;
using Eum.Extensions.Logging;
using Eum.gRPC.Server.ServiceDesk;
using Eum.gRPC.Server.ServiceDesk.Modules.ClientModule;
using Eum.gRPC.Server.ServiceDesk.Modules.CompanyModule;
using Eum.gRPC.Server.ServiceDesk.Modules.ContractModule;
using Eum.gRPC.Server.ServiceDesk.Modules.ProductModule;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Server;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
//builder.ConfigureKestrelWithRandomPort();

// To configure your ASP.NET Core application to run as a Windows service,
// install the Microsoft.Extensions.Hosting.WindowsServices package from NuGet. Then add a call to UseWindowsService 
builder.Host.UseWindowsService();
// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddCodeFirstGrpc();
builder.Services.AddCodeFirstGrpcReflection();
builder.Services.AddAuthorization();
builder.Services.AddJwtAuth();
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.AddMapper();
builder.Host.UseEumLogging();
new SharedModule().ConfigureServices(builder.Services);

var app = builder.Build();
app.ConfigureEumCore();
new SharedModule().ApplicationLoaded(app.Services);

app.UseMiddleware<AuthorizationHeaderMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
//app.MapGrpcService<CaseEndpoint>();
app.MapGrpcService<ClientEndpoint>();
app.MapGrpcService<CompanyEndpoint>();
app.MapGrpcService<ContractEndpoint>();
app.MapGrpcService<ProductEndpoint>();
app.MapCodeFirstGrpcReflectionService();
app.MapGet("/", () => $"Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
