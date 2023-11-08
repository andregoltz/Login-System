using Login_System.Core.Contexts.AccountContext.Entities;
using Login_System.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using MediatR;

namespace Login_System.Core.Contexts.AccountContext.UseCases.Authenticate
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IRepository _repository;

        public Handler(IRepository repository) => _repository = repository;
       
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            #region 01. Request Validation

            try
            {
                var res = Specification.Ensure(request);

                if (!res.IsValid)
                    return new Response("Requisição inválida", 400, res.Notifications);
            }
            catch
            {
                return new Response("Não foi possível validar sua requisição", 500, null);
            }

            #endregion

            #region 02. Load User
            User? user;
            try
            {
                user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);

                if (user is null)
                    return new Response("Perfil não encontrado", 404);
            }
            catch (Exception)
            {

                return new Response("Não foi possível recuperar seu perfil", 500);
            }
            #endregion

            #region 03. Password is Valid?
            if (!user.Password.Challenge(request.Password))
                return new Response("Usuário ou senha inválidos", 400);

            #endregion

            #region 04. Account is Active?
            try
            {
                if (!user.Email.Verification.IsActive)
                    return new Response("Conta inativa", 400);
            }
            catch (Exception)
            {
                return new Response("Não foi possível verificar seu perfil", 500);
            }

            #endregion

            #region 05. Returning Data
            try
            {
                var data = new ResponseData 
                {
                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Email = user.Email,
                    Roles = Array.Empty<string>()
                };

                return new Response(string.Empty, data);
            }
            catch (Exception)
            {
                return new Response("Não foi possível obter os dados do perfil", 500);
            }
            #endregion
        }
    }
}
