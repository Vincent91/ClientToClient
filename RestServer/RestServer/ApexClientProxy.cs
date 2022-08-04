using System.Net.Http.Headers;
using System.Text.Json;

namespace RestServer;

public class ApexClientProxy
{

    private readonly string _url;
    
    private readonly HttpClient _client;
    private readonly HttpClientHandler _handler;

    private string _name;
    
    public ApexClientProxy(string url)
    {
        _url = url;
        
        _handler = new HttpClientHandler();
        _handler.UseDefaultCredentials = true;
        
        _client = new HttpClient(_handler);
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        _client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
    }

    public void Connect()
    {
        var json = _client.GetStringAsync(_url).Result;
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        var tokenList = JsonSerializer.Deserialize<List<Token>>(json, options) ?? new List<Token>();
        var token = tokenList[0];
        
        _name = token.Name ?? "Null";
        Console.WriteLine(_name);
    }

    public Task<string> RequestData(string data)
    {
        Console.WriteLine($"Requested data [{data}]");
        Console.WriteLine($"Adding Header [{_name}]");
        var uri = _url + data;
        return _client.GetStringAsync(uri);
    }

}