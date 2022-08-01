﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dto.Project;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Application.Project
{
    public class List
    {
        public class Query : IRequest<IReadOnlyList<ProjectDto>>
        {

        }
        public class Handler : IRequestHandler<Query, IReadOnlyList<ProjectDto>>
        {
            private readonly IServiceProvider _serviceProvider;

            public Handler(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }

            public async Task<IReadOnlyList<ProjectDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<IDataContext>() ?? throw new InvalidOperationException();
                    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>() ?? throw new InvalidOperationException();
                    var projects = await context.Projects.ToListAsync(cancellationToken);

                    return mapper.Map<List<Domain.Project>, IReadOnlyList<ProjectDto>>(projects);
                }
            }
        }
    }
}
