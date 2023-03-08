using Eum.gRPC.Server.Mail;
using Grpc.Net.Client;




// The port number must match the port of the gRPC server.
const string endpoint = "https://localhost";
using var channel = GrpcChannel.ForAddress($"{endpoint}:8402/");


var client = new Mail.MailClient(channel);
var req = new MailRequest { MailId = 0 };

while (true)
{
    var dealy = Task.Delay(100);
    dealy.Wait();

    var reply = await client.GetMailListAsync(req);
    Console.WriteLine("Greeting: " + reply.Message);
    Console.WriteLine("Press any key to exit...");
    //Console.ReadKey();

}

