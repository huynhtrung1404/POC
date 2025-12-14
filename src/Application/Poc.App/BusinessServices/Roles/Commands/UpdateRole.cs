using Poc.App.BusinessServices.Roles.Models;
using Poc.Mediator;

namespace Poc.App.BusinessServices.Roles.Commands;

public record UpdateRole(RoleDto Model) : IRequest<bool>;

public class UpdateRoleHandler(IRepository<Role> roleRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateRole, bool>
{
    private readonly IRepository<Role> _roleRepository = roleRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> HandleAsync(UpdateRole request, CancellationToken cancellationToken = default)
    {
        var data = new Role
        {
            Id = request.Model.Id,
            RoleName = request.Model.RoleName,
            Description = request.Model.Description
        };
        _roleRepository.Update(data);
        return await _unitOfWork.SaveChangeAsync();
    }
}