using Login_System.Core.Contexts.AccountContext.Entities;

namespace Login_System.Core.Contexts.AccountContext.UseCases.Create.Contracts
{
    public interface IService
    {
        Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken);
    }
}
