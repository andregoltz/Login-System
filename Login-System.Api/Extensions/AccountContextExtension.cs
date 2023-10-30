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
        }

        public static void MapAccountEndpoints(this WebApplication app)
        {
            #region Create



            #endregion
        }
    }
}
