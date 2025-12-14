namespace Poc.App.BusinessServices.Managements.AuthConfigs;

public class AuthConfigDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ProviderName { get; set; }
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public string? Authority { get; set; }
    public string? RedirectUri { get; set; }
    public string? PostLogoutRedirectUri { get; set; }
    public string? Scopes { get; set; }
    public string? AdditionParam { get; set; }
    public bool IsActive { get; set; }
    public string? TenantId { get; set; }
    public string? Domain { get; set; }
    public string? Audience { get; set; }
    public string? Description { get; set; }
}