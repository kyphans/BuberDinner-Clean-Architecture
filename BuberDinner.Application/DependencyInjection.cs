using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BuberDinner.Application.Commands.GeneralCommands;
using BuberDinner.Application.Queries.GeneralQueries;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            services.AddScopedMediatR<User>();
            return services;
        }
        public static IServiceCollection AddScopedMediatR<TEntity>(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<Add<TEntity>, int>, AddHandler<TEntity>>();
            services.AddTransient<IRequestHandler<Remove<TEntity>, int>, RemoveHandler<TEntity>>();

            services.AddTransient<IRequestHandler<FindBy<TEntity>, IQueryable<TEntity>>, FindByHandler<TEntity>>();
            return services;
        }
    }
}
