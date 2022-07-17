using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Application.Project
{
    public class List
    {
        public class Query : IRequest<IReadOnlyList<Domain.Project>>
        {

        }
        public class Handler : IRequestHandler<Query, IReadOnlyList<Domain.Project>>
        {
            private readonly IServiceProvider _serviceProvider;

            public Handler(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }

            public async Task<IReadOnlyList<Domain.Project>> Handle(Query request, CancellationToken cancellationToken)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<IDataContext>() ?? throw new InvalidOperationException();
                    var projects = await context.Projects.ToListAsync(cancellationToken);

                    return projects.AsReadOnly();
                }
            }
        }
    }
}
