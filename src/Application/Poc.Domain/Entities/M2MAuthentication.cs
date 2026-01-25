using Poc.Domain.Enums;

namespace Poc.Domain.Entities;

public class M2MAuthentication : BaseEntity
{
    public string? Url { get; set; }
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public string? Scope { get; set; }
    public GrantType GrantType { get; set; }
    public LoginType LoginType { get; set; }
}