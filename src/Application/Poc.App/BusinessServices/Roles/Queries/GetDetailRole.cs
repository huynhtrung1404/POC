using Poc.App.BusinessServices.Roles.Models;
using Poc.App.BusinessServices.Roles.Specifications;
using Poc.Mediator;

namespace Poc.App.BusinessServices.Roles.Queries;

public record GetDetailRole(Guid Id) : IRequest<RoleDto>;

public class GetDetailRoleHandler(IRepository<Role> roleRepository) : IRequestHandler<GetDetailRole, RoleDto>
{
    private readonly IRepository<Role> _roleRepository = roleRepository;

    public async Task<RoleDto> HandleAsync(GetDetailRole request, CancellationToken cancellationToken = default)
    {
        var data = await _roleRepository.GetItemAsync(new RoleSpecification(request.Id)) ?? throw new Exception();
        return new RoleDto
        {
            Id = data.Id,
            RoleName = data.RoleName,
            Description = data.Description
        };
    }
}