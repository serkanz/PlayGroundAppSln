using Dto.Project;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ProjectController : BaseController<ProjectController>
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProjectDto>>> Get(CancellationToken cancellationToken)
        {
            var projects = await Mediator.Send(new Application.Project.List.Query(), cancellationToken);

            return Ok(projects);
        }
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> Post(ProjectDto projectDto, CancellationToken cancellationToken)
        {
            var projectCreated = await Mediator.Send(new Application.Project.Create.Command() { ProjectDto = projectDto }, cancellationToken);

            return Ok(projectCreated);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                await Mediator.Send(new Application.Project.Delete.Command() { Id = id });
                return Ok(true);
            }
            catch (NullReferenceException exception)
            {
                Logger.LogError(exception.StackTrace);
                return BadRequest($"Project with id {id} not found in the database");
            }
            catch (Exception exception)
            {
                Logger.LogError(exception.StackTrace);
                throw;
            }
        }
    }
}
