using Poc.App.BusinessServices.Roles.Models;
using Poc.Mediator;

namespace Poc.App.BusinessServices.Roles.Queries;

public record GetAllRole : IRequest<IEnumerable<RoleDto>>;

public class GetAllRoleHandler(IRepository<Role> roleRepository) : IRequestHandler<GetAllRole, IEnumerable<RoleDto>>
{
    private readonly IRepository<Role> _roleRepository = roleRepository;

    public async Task<IEnumerable<RoleDto>> HandleAsync(GetAllRole request, CancellationToken cancellationToken = default)
    {
        var response = await _roleRepository.GetAllAsync();
        var result = response.Select(x => new RoleDto
        {
            Description = x.Description,
            RoleName = x.RoleName,
            Id = x.Id
        });
        return result;
    }
}