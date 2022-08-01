using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dto.Project;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Application.Project
{
    public class Create
    {
        public class Command : IRequest<ProjectDto>
        {
            public ProjectDto ProjectDto { get; set; }
        }

        public class Handler : IRequestHandler<Command,ProjectDto>
        {
            private readonly IServiceProvider _serviceProvider;

            public Handler(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }

            public async Task<ProjectDto> Handle(Command request, CancellationToken cancellationToken)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<IDataContext>() ?? throw new InvalidOperationException();
                    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>() ?? throw new InvalidOperationException();

                    var project = mapper.Map<ProjectDto,Domain.Project>(request.ProjectDto);

                    await context.Projects.AddAsync(project, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);
                    return mapper.Map<Domain.Project, ProjectDto>(project);
                }
            }
        }
    }
}
