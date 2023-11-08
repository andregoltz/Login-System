using MediatR;
using System.Runtime.CompilerServices;

namespace Login_System.Api.Extensions
{
    public static class AccountContextExtension
    {
        public static void AddAccountContext(this WebApplicationBuilder builder) 
        {
            #region Create
            builder.Services.AddTransient<
                Login_System.Core.Contexts.AccountContext.UseCases.Create.Contracts.IRepository,
                Login_System.Infra.Contexts.AccountContext.UseCases.Create.Repository>();

            builder.Services.AddTransient<
                Login_System.Core.Contexts.AccountContext.UseCases.Create.Contracts.IService,
                Login_System.Infra.Contexts.AccountContext.UseCases.Create.Service>();
            #endregion

            #region Authenticate
            builder.Services.AddTransient<
                Login_System.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts.IRepository,
                Login_System.Infra.Contexts.AccountContext.UseCases.Authenticate.Repository>();

            #endregion
        }

        public static void MapAccountEndpoints(this WebApplication app)
        {
            #region Create
            app.MapPost("api/v1/users", async (
                Login_System.Core.Contexts.AccountContext.UseCases.Create.Request request,
                IRequestHandler<
                    Login_System.Core.Contexts.AccountContext.UseCases.Create.Request,
                    Login_System.Core.Contexts.AccountContext.UseCases.Create.Response> handler) => 
            {
                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Created($"api/v1/users/{result.Data?.Id}", result)
                    : Results.Json(result, statusCode: result.Status);
            });
            #endregion

            #region Authenticate
            app.MapPost("api/v1/authenticate", async (
                Login_System.Core.Contexts.AccountContext.UseCases.Authenticate.Request request,
                IRequestHandler<
                    Login_System.Core.Contexts.AccountContext.UseCases.Authenticate.Request,
                    Login_System.Core.Contexts.AccountContext.UseCases.Authenticate.Response> handler) =>
            {
                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.Status);
            });
            #endregion
        }
    }
}
