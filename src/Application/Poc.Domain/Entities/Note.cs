namespace Poc.Domain.Entities;

public class Note : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public string? AdditionInfo { get; set; }
    public string? Tag { get; set; }
}