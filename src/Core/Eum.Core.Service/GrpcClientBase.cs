using Grpc.Core;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service
{
    public abstract class GrpcClientBase : IDisposable
    {
        // To detect redundant calls
        private bool _disposed;
        private GrpcChannel? _grpcChannel;
        private string _token;


        protected T CreateClient<T>(string token) where T : class, IEndpoint
        {
            if (_disposed) throw new ObjectDisposedException("GrpcChannel");
            _token = token;
            EnsureChannel();
            return _grpcChannel!.CreateGrpcService<T>();
        }

        protected void EnsureChannel()
        {
            if (_grpcChannel == null)
                _grpcChannel = CreateAuthenticatedChannel(GetAddress());
        }

        private GrpcChannel CreateAuthenticatedChannel(string address)
        {
            var credentials = CallCredentials.FromInterceptor((context, metadata) =>
            {
                if (!string.IsNullOrEmpty(_token))
                {
                    metadata.Add("Authorization", _token.StartsWith("Bearer ") ? _token : $"Bearer {_token}");
                }
                return Task.CompletedTask;
            });
            var options = new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Create(new SslCredentials(), credentials)
            };

            var channel = GrpcChannel.ForAddress(address, options);
            return channel;
        }

        protected abstract string GetAddress();

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // dispose managed state (managed objects).
                if (_grpcChannel != null)
                {
                    _grpcChannel.Dispose();
                    _grpcChannel = null;
                }
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposed = true;
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }

}
