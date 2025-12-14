using Poc.Mediator;

namespace Poc.App.BusinessServices.Roles.Commands;

public record DeleteRole(Guid Id) : IRequest<bool>;

public class DeleteRoleHandler(IRepository<Role> roleRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteRole, bool>
{
    private readonly IRepository<Role> _roleRepository = roleRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> HandleAsync(DeleteRole request, CancellationToken cancellationToken = default)
    {
        await _roleRepository.DeleteAsync(request.Id);
        return await _unitOfWork.SaveChangeAsync();
    }
}