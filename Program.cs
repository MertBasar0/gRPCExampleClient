using Grpc.Core;
using Grpc.Net.Client;
using gRPCExample;
using System.Linq.Expressions;

GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7289", new GrpcChannelOptions() { MaxReceiveMessageSize = 2000000000});
var client = new ExampleRPCServices.ExampleRPCServicesClient(channel);
Bogus.Faker faker = new Bogus.Faker();




#region bidirectional
var bidirectionalWay = client.BidirectionalStreamingMethod();
for (int i = 0; i < 3; i++)
{
    var blurredImage = faker.Image.PicsumUrl(width:2560, height: 1440);
    await bidirectionalWay.RequestStream.WriteAsync(new bidirectionalWayExampleRequest { ClientSideImage = blurredImage });
}

await bidirectionalWay.RequestStream.CompleteAsync();

await foreach (var item in bidirectionalWay.ResponseStream.ReadAllAsync())
{
    Console.WriteLine(item.ManipulatedServerSideImage);
}
#endregion
#region ClientStream
//var request = new exampleRequest()
//{
//Name = "Aysun",
//Surname = "Başar"
//};


//var clientStream = client.ClientStreamMethod();
//for (int i = 0; i < 30; i++)
//{
//await clientStream.RequestStream.WriteAsync(request);

//}
//await clientStream.RequestStream.CompleteAsync();
//var response = await clientStream.ResponseAsync;
//Console.WriteLine(response.ToString());


#endregion
#region ServerStream
//var triggerMessage = new emptyMessageForTrigger();

//var res = client.ServerStreamMethod(request);

//int index = 0;

//await foreach (var item in res.ResponseStream.ReadAllAsync())
//{
//    index++;
//    Console.WriteLine($"{item}\nişlem bitti\n\n\n\n\n{index}");
//}

//Console.WriteLine($"{res}\nişlem bitti");
#endregion

Console.ReadLine();

