using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using MappingProfiles;
using Microsoft.AspNetCore.Http.Json;
using Utils;

var builder = WebApplication.CreateBuilder(args);

var serviceCollection = builder.Services;
serviceCollection.AddLogging(loggingBuilder => loggingBuilder.AddConsole());
serviceCollection.AddDbContext<IDataContext, DataContext>(optionsBuilder => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=playgrounddb;Username=playgrounduser;Password=**12qwas**"));
serviceCollection.AddAutoMapper(typeof(ProjectMapping));
serviceCollection.AddMediatR(typeof(Application.Project.List.Handler).Assembly);
serviceCollection.AddAuthentication();
serviceCollection.AddAuthorization();
serviceCollection.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<DataContext>().Database.Migrate();
}

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(routeBuilder => routeBuilder.MapControllers());
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlayGroundApp API V1");
});
app.Run();


