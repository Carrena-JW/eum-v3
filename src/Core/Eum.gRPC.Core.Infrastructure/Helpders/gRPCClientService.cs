using Eum.gRPC.Server.Common;
using Eum.gRPC.Server.Mail;
using Eum.gRPC.Shared.Enums;
using Eum.gRPC.Shared.Helpers;
using Grpc.Net.Client;
using System.Net.Http;
using System.Net.Sockets;

namespace Eum.gRPC.Shared.Helpders
{
    public class gRPCClientService
    {
        private readonly EnumEndpointTarget _target;
        public gRPCClientService(EnumEndpointTarget target)
        {
            _target = target;
        }

        public bool IsLoginUser()
        {
            var isIPC = false;
            // IPC , RPC 분기
            var socketPath = Path.Combine(Path.GetTempPath(), "eum_socket_common.tmp");
            if (File.Exists(socketPath))
            {
                isIPC = true;
            }


            SocketsHttpHandler socketsHttpHandler;
            GrpcChannelOptions channelOptions;

           
                
                var udsEndPoint = new UnixDomainSocketEndPoint(socketPath);
                var connectionFactory = new SoketConnectionFactory(udsEndPoint);
                 socketsHttpHandler = new SocketsHttpHandler
                {
                    ConnectCallback = connectionFactory.ConnectAsync
                };

                channelOptions = new GrpcChannelOptions
                {
                    HttpHandler = socketsHttpHandler
                };
          

            using var channel = GrpcChannel.ForAddress(EndpointManager.GetEndpoint(_target), channelOptions);
            // 




            var client = new Authentication.AuthenticationClient(channel);
            var req = new AuthenticationRequest { Email = "asdfadfadfadf" };

            var reply = client.Login(req);
            return reply.Result;
        }
    }
}
