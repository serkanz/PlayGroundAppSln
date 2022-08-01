using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using NullReferenceException = System.NullReferenceException;

namespace Application.Project
{
    public class Delete
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IServiceProvider _serviceProvider;

            public Handler(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<IDataContext>() ?? throw new InvalidOperationException();

                    var project = await context.Projects.SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);

                    if (project == null)
                        throw new NullReferenceException();

                    context.Projects.Remove(project);
                    await context.SaveChangesAsync(cancellationToken);

                    return Unit.Value;
                }
            }
        }
    }
}
