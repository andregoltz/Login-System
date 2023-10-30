using Login_System.Core.AccountContext.ValueObjects;
using Login_System.Core.Contexts.AccountContext.Entities;
using Login_System.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using Login_System.Core.Contexts.AccountContext.ValueObjects;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace Login_System.Core.Contexts.AccountContext.UseCases.Create
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IRepository _repository;
        private readonly IService _service;

        public Handler(IRepository repository, IService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<Response> Handle(
            Request request,
            CancellationToken cancellationToken)
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

            #region 02. Objects Generation
            Email email;
            Password password;
            User user;

            try
            {
                email = new Email(request.Email);
                password = new Password(request.Password);
                user = new User(request.Name, email, password);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, 400);
            }
            #endregion

            #region 03. Entity Verification

            try
            {
                var exists = await _repository.AnyAsync(request.Email, cancellationToken);

                if (exists)
                    return new Response("Este E-mail já está em uso", 400);
            }
            catch
            {
                return new Response("Falha ao verificar o E-mail cadastrado", 500);
            }

            #endregion

            #region 04. Saving the Data
            try
            {
                await _repository.SaveAsync(user, cancellationToken);
            }
            catch
            {
                return new Response("Falha ao persistir dados", 500);
            }
            #endregion

            #region 05. Send Activation Email
            try
            {
                await _service.SendVerificationEmailAsync(user, cancellationToken);
            }
            catch
            { 
                //Do nothing
            }
            #endregion

            return new Response("Conta criada", new ResponseData(user.Id, user.Name, user.Email));
        }
    }
}
