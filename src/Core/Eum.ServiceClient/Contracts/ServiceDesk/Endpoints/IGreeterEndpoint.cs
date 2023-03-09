using Eum.ServiceClient.Contracts.ServiceDesk.Data.Case;
using ProtoBuf.Grpc;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Eum.ServiceClient.Contracts.ServiceDesk.Endpoints
{

    [DataContract]
    public class HelloReply
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }

        [DataMember(Order = 2)]
        public CaseInfo[] Items { get; set; }
    }

    [DataContract]
    public class HelloRequest
    {
        [DataMember(Order = 1)]
        public string Name { get; set; }
    }

    [ServiceContract]
    public interface IGreeterEndpoint
    {
        [OperationContract]
        Task<HelloReply> SayHello2Async(HelloRequest request, CallContext context = default);
    }
}