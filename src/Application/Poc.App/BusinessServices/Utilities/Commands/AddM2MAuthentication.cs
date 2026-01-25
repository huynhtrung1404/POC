using System.Runtime.CompilerServices;
using Poc.Domain.Enums;

namespace Poc.App.BusinessServices.Utilities.Commands;

public class AddM2MAuthentication
{
    public record Request(RequestBody Body) : IRequest<bool>;
    public class RequestBody
    {
        public string? Url { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? Scope { get; set; }
        public GrantType GrantType { get; set; }
        public LoginType LoginType { get; set; }
    }

    public class Handler(IRepository<M2MAuthentication> repository, IUnitOfWork unitOfWork) : IRequestHandler<Request, bool>
    {
        private readonly IRepository<M2MAuthentication> _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> HandleAsync(Request request, CancellationToken cancellationToken = default)
        {
            var data = new M2MAuthentication
            {
                ClientId = request.Body.ClientId,
                ClientSecret = request.Body.ClientSecret,
                GrantType = request.Body.GrantType,
                LoginType = request.Body.LoginType,
                Scope = request.Body.Scope,
                Url = request.Body.Url
            };
            await _repository.AddAsync(data);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}