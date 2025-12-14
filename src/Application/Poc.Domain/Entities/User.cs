namespace Poc.Domain.Entities;

public class User : BaseEntity
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public int Age { get; set; }
    public string? Address { get; set; }
    public string? Permission { get; set; }
    public bool IsDisabled { get; set; }
    public IEnumerable<Token> Tokens { get; set; } = [];
    public IEnumerable<Role> Roles { get; set; } = [];
}