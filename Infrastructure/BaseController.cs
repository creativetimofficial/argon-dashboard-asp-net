using Microsoft.AspNetCore.Mvc;

namespace CreativeTim.Argon.DotNetCore.Free.Infrastructure
{
    [Route("[controller]/[action]", Name = "[controller]_[action]")]
    public abstract class BaseController : Controller
    {
    }
}
