using Eum.gRPC.Server.Common.Endpoints;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.WebHost.ConfigureKestrel(serverOptions =>
{

    var socketPath = Path.Combine(Path.GetTempPath(), "eum_socket_common.tmp");
    if (File.Exists(socketPath))
    {
        File.Delete(socketPath);
    }

    serverOptions.ListenUnixSocket(socketPath, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
        listenOptions.UseHttps();
    });

});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<AuthenticationEndpoint>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
