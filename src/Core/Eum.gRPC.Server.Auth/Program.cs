using Eum.Core;
using Eum.Core.Module;
using Eum.Extensions.Logging;
using Eum.gRPC.Server.Auth.Endpoints.Auth;
using Eum.gRPC.Server.Auth.Endpoints.Token;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddCodeFirstGrpc();
builder.Services.AddCodeFirstGrpcReflection();
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Host.UseEumLogging();

var app = builder.Build();
app.ConfigureEumCore();

var logger = app.Services.GetService<ILogger<Program>>();
logger!.LogInformation("Test!!");

// Configure the HTTP request pipeline.
app.MapGrpcService<AuthEndpoint>();
app.MapGrpcService<TokenEndpoint>();
app.MapCodeFirstGrpcReflectionService();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
