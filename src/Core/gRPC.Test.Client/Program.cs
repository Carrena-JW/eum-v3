 

using Eum.gRPC.Server.Proto.Mail;
using Grpc.Net.Client;
 

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://stone.feelanet.com:801/rpcMail");
 
var client = new Mail.MailClient(channel);
var req = new MailRequest { MailId = 0 };
var reply = await client.GetMailListAsync(req);
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

