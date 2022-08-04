using Microsoft.AspNetCore.Mvc;

namespace RestServer;

[Produces("application/json")]
public class ApexController : Controller
{

    private ApexClientProxy _proxy;

    public ApexController(ApexClientProxy proxy)
    {
        _proxy = proxy;
    }

    [HttpGet]
    public Task<string> RetrieveData([FromQuery] string query)
    {
        return _proxy.RequestData(query);
    }

}