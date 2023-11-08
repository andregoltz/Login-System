using Login_System.Core.Contexts.AccountContext.Entities;
using Login_System.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using Login_System.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Login_System.Infra.Contexts.AccountContext.UseCases.Authenticate
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context) => _context = context;
        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken) => await _context
                        .Users
                        .AsNoTracking()
                        .Include(x => x.Roles)
                        .FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);
    }
}
