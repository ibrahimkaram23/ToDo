using Microsoft.EntityFrameworkCore;
using ToDo.Infrastructure.Context;
using ToDo.Infrastructure.DI;
using ToDo.Application.DI;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json.Serialization;
using ToDo.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.InfrastractureStrapping();

builder.Services.ApplicationStrapping();

builder.Services.AddDbContext<ApplicationDBContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
sqlServerOptionsAction: options => {
    options.EnableRetryOnFailure();
    options.CommandTimeout(10);
}));

builder.Services.AddIdentityServices(builder.Configuration);

//await builder.Services.AddRolesAsync();

builder.Services.AddMappingService();

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.DocumentTitle = "ToDo API Documentation V1";
    options.DocExpansion(DocExpansion.None);
    options.DisplayRequestDuration();
});

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors(options =>
{
    options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
