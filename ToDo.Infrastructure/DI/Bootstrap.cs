using Microsoft.Extensions.DependencyInjection;
using ToDo.Domain.Abstractions;
using ToDo.Infrastructure.Repositories;

namespace ToDo.Infrastructure.DI
{
    public static class Boostrap
    {
        public static void InfrastractureStrapping(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
