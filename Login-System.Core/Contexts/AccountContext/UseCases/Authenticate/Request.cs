using MediatR;

namespace Login_System.Core.Contexts.AccountContext.UseCases.Authenticate
{
    public record Request(
        string Email,
        string Password) : IRequest<Response>;
}
