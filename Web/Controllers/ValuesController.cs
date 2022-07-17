using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ValuesController : BaseController
    {
        [HttpGet]
        public async Task<int> Get()
        {
            await Task.Delay(1000);

            return 0;
        }
    }
}
