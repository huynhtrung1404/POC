namespace Poc.App.BusinessServices.Managements.Users;

public class UserDto
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Permission { get; set; }
    public int Age { get; set; }
    public string? Address { get; set; }
    public bool IsDisabled { get; set; }
}