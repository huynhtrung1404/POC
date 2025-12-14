namespace Poc.Domain.Entities;

public class Role : BaseEntity
{
    public string? RoleName { get; set; }
    public string? Description { get; set; }
    public IEnumerable<User> Users { get; set; } = [];
}