using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

var serviceCollection = builder.Services;
serviceCollection.AddDbContext<DataContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=playgrounddb;Username=playgrounduser;Password=**12qwas**");

});
serviceCollection.AddAuthentication();
serviceCollection.AddAuthorization();
serviceCollection.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<DataContext>().Database.Migrate();
}

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(routeBuilder => routeBuilder.MapControllers());

app.Run();
