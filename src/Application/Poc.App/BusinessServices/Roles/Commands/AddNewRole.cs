using Poc.App.BusinessServices.Roles.Models;
using Poc.Mediator;

namespace Poc.App.BusinessServices.Roles.Commands;

public record AddNewRole(AddRoleDto Model) : IRequest<bool>;

public class AddNewRoleHandler(IRepository<Role> roleRepository, IUnitOfWork unitOfWork) : IRequestHandler<AddNewRole, bool>
{
    private readonly IRepository<Role> _roleRepository = roleRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> HandleAsync(AddNewRole request, CancellationToken cancellationToken = default)
    {
        var data = new Role
        {
            RoleName = request.Model.RoleName,
            Description = request.Model.Description
        };

        await _roleRepository.AddAsync(data);
        return await _unitOfWork.SaveChangeAsync();
    }
}