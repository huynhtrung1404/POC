namespace Poc.App.Models;

public class PagingResponse<T>
{
    public IEnumerable<T> Result { get; set; } = [];
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int Total { get; set; }
}