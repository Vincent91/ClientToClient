using RestServer;

var apexProxy = new ApexClientProxy("http://localhost:5073/trades");

apexProxy.Connect();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapGet("/apexdata", (context) =>
{
    var query = context.Request.QueryString;
    var promise = apexProxy.RequestData(query.ToString());
    var result = promise.Result;
    Console.WriteLine($"Received [{result}]");
    return Task.CompletedTask;
});

app.Run();