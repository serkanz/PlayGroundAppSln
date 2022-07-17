using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ProjectController : BaseController
    {
        [HttpGet]
        public async Task<IReadOnlyList<Project>> Get()
        {
            var projects = await Mediator.Send(new Application.Project.List.Query());

            return projects;
        }
    }
}
