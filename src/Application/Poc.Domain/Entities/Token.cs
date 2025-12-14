namespace Poc.Domain.Entities;

public class Token : BaseEntity
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime ExpireTime { get; set; }
    public bool IsDeleted { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
}